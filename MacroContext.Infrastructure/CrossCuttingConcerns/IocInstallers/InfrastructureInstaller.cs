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
using MacroContext.HostContract;
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



            _simpleContainer.RegisterSingleton<ConnectionFactory>(() => GetRabbitMQConnectionFactory());


            _simpleContainer.RegisterSingleton<ServiceHosts>();
            _simpleContainer.RegisterSingleton<IServiceHosts>(() => _simpleContainer.GetInstance<ServiceHosts>());

            _simpleContainer.Register<ICommandProcessor, CommandProcessor>(Lifestyle.Transient);
            _simpleContainer.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Transient);
            _simpleContainer.RegisterSingleton<lEventPublisher>(new EventPublisher());
            _simpleContainer.Register<RequestOrchestrator>(Lifestyle.Transient);


            // command processor decorators
            _simpleContainer.RegisterDecorator<ICommandProcessor, EventPublisherCommandProcessorDecorator>();
            _simpleContainer.RegisterDecorator<ICommandProcessor, TransactionScopeCommandProcessorDecorator>();
            _simpleContainer.RegisterDecorator<ICommandProcessor, LifeTimeScopeCommandProcessorDecorator>();


            // query processor decorators
            _simpleContainer.RegisterDecorator<IQueryProcessor, LifeTimeScopeQueryProcessorDecorator>();



        }

        private static ConnectionFactory GetRabbitMQConnectionFactory()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString;
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(connectionString);
            return factory;

        }


    }
}
