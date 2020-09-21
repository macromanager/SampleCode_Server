using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using System.Data.Entity;
using AutoMapper;
using SimpleInjector.Diagnostics;
using MacroContext.Infrastructure.Abstractions.Mapper;
using MacroContext.Shared.Utilities;
using RabbitMQ.Client;
using System.Configuration;
using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Infrastructure.Abstractions;
using MacroContext.Infrastructure.CrossCuttingConcerns;
using MacroContext.Infrastructure.MessengerServices;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Infrastructure;
using MacroContext.Shared.Utilities.Logger;
using MacroContext.Infrastructure.Abstractions.Loggers;
//using FinanceManager.Contract.Services;


namespace Infrastructure.IocInstallers
{
    static class InfrastructureInstaller
    {
        private static Container _container;
        public static void RegisterServices(Container _simpleContainer)
        {
            _container = _simpleContainer;
            _simpleContainer.RegisterSingleton<MapperConfiguration>(AutoMapperConfiguration.MapConfig);
            _simpleContainer.Register<IMapper>(() => _simpleContainer.GetInstance<MapperConfiguration>().CreateMapper());
            _simpleContainer.Register<IMyMapper, MyMapper>();

            _simpleContainer.Register<ILogger, SerilogAdapter>(Lifestyle.Singleton);

            _simpleContainer.RegisterSingleton<ConnectionFactory>(() => GetRabbitMQConnectionFactory());


            _simpleContainer.RegisterSingleton<ServiceHosts>();

            _simpleContainer.Register<ICommandProcessor, CommandProcessor>(Lifestyle.Transient);
            _simpleContainer.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Transient);
            _simpleContainer.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Transient);
            _simpleContainer.Register<IQueryDispatcher, QueryDispatcher>(Lifestyle.Transient);

            _simpleContainer.Register<EventPublisher>(Lifestyle.Scoped);
            _simpleContainer.Register<lEventPublisher>(() => _simpleContainer.GetInstance<EventPublisher>());
            _simpleContainer.Register<RequestOrchestrator>(Lifestyle.Transient);


            // command processor decorators
            _simpleContainer.RegisterDecorator<ICommandProcessor, EventPublisherCommandProcessorDecorator>();
            _simpleContainer.RegisterDecorator<ICommandProcessor, TransactionScopeCommandProcessorDecorator>();
            //_simpleContainer.RegisterDecorator<ICommandProcessor, LifeTimeScopeCommandProcessorDecorator>();


            // query processor decorators
            _simpleContainer.RegisterDecorator<IQueryProcessor, LifeTimeScopeQueryProcessorDecorator>();



        }

        private static ConnectionFactory GetRabbitMQConnectionFactory()
        {
            var connectionString = AppSettings.RABBITMQ_CONNECTION_STRING;
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(connectionString);
            return factory;

        }


    }
}
