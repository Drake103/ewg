using System.Data;
using NHibernate;

namespace EWG.Infrastructure.Dal.Impl
{
    public class NhUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;

        public NhUnitOfWork(ISession session)
        {
            _session = session;
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        public IGenericTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            var tx = _session.BeginTransaction(isolationLevel);
            return new GenericTransaction(tx);
        }

        public void Clear()
        {
            _session.Clear();
        }

        public void Flush()
        {
            _session.Flush();
        }

        public ISession GetSession()
        {
            return _session;
        }
    }
}