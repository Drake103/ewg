using System;
using EWG.Domain.Entities;
using NHibernate;
using NHibernate.Cfg;

namespace EWG.Infrastructure.Dal.Impl
{
    public class NhUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _configurationFile;

        private ISessionFactory _sessionFactory;

        public NhUnitOfWorkFactory(string configurationFile = null)
        {
            _configurationFile = configurationFile;
        }

        private ISessionFactory SessionFactory
        {
            get
            {
                lock (this)
                {
                    if (_sessionFactory == null)
                    {
                        BuildSessionFactory();
                    }
                }

                return _sessionFactory;
            }
        }

        private void BuildSessionFactory()
        {
            try
            {
                var config = new Configuration();

                if (string.IsNullOrEmpty(_configurationFile))
                {
                    config.Configure();
                }
                else
                {
                    config.Configure(_configurationFile);
                }

                config.AddAssembly(typeof (NhUnitOfWork).Assembly);

                _sessionFactory = config.BuildSessionFactory();
            }
            catch (Exception)
            {
                _sessionFactory = null;
                throw;
            }
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new NhUnitOfWork(SessionFactory.OpenSession());
        }
    }
}