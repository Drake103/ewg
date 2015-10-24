using Autofac;
using EWG.Infrastructure.Dal;
using EWG.Infrastructure.Dal.Impl;
using EWG.Infrastructure.Dal.Impl.Repositories;
using EWG.Infrastructure.Services.Impl.Common;
using EWG.Infrastructure.Services.Impl.ReplayParsing;
using EWG.Infrastructure.Services.Impl.Replays;

namespace EWG.DependencyResolver
{
    public class AutofacContainerBuilderForTests
    {
        private readonly string _configurationFilePath;

        public AutofacContainerBuilderForTests(string configurationFilePath)
        {
            _configurationFilePath = configurationFilePath;
        }

        private ContainerBuilder _builder;

        public ContainerBuilder Get()
        {
            _builder = new ContainerBuilder();

            RegisterTypes();

            return _builder;
        }

        private void RegisterTypes()
        {
            RegisterUow();
            RegisterRepositories();
            RegisterServices();
        }

        private void RegisterUow()
        {
            var factory = new NhUnitOfWorkFactory(_configurationFilePath);
            
            _builder.Register(c => factory.CreateUnitOfWork())
                .As<IUnitOfWork>().InstancePerLifetimeScope();

            _builder.RegisterGeneric(typeof(CrudRepository<>))
                .As(typeof(ICrudRepository<>)).InstancePerLifetimeScope();

            _builder.RegisterType<DbInitializer>().AsImplementedInterfaces();
        }

        private void RegisterRepositories()
        {
            _builder.RegisterGeneric(typeof(GenericDictionaryItemRepository<>)).AsImplementedInterfaces();
            _builder.RegisterType<ReplayRepository>().AsImplementedInterfaces();
            _builder.RegisterType<PlayerUserRepository>().AsImplementedInterfaces();
        }

        private void RegisterServices()
        {
            _builder.RegisterType<ReplayParser>().AsImplementedInterfaces();
            _builder.RegisterType<ReplayMapper>().AsImplementedInterfaces();
            _builder.RegisterType<ReplayService>().AsImplementedInterfaces();
            _builder.RegisterType<DatabaseService>().AsImplementedInterfaces();
        }
    }
}
