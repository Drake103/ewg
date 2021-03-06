﻿using Autofac;
using Autofac.Integration.Mvc;
using EWG.Infrastructure.Dal;
using EWG.Infrastructure.Dal.Impl;
using EWG.Infrastructure.Dal.Impl.Repositories;
using EWG.Infrastructure.Services.Impl.Common;
using EWG.Infrastructure.Services.Impl.ReplayParsing;
using EWG.Infrastructure.Services.Impl.Replays;

namespace EWG.DependencyResolver
{
    public class AutofacContainerBuilder
    {
        private readonly string _nhConfigurationFilePath;

        public AutofacContainerBuilder(string nhConfigurationFilePath)
        {
            _nhConfigurationFilePath = nhConfigurationFilePath;
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
            var factory = new NhUnitOfWorkFactory(_nhConfigurationFilePath);
            
            _builder.Register(c => factory.CreateUnitOfWork())
                .As<IUnitOfWork>().InstancePerHttpRequest();

            _builder.RegisterGeneric(typeof(CrudRepository<>))
                .As(typeof(ICrudRepository<>)).InstancePerHttpRequest();

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
