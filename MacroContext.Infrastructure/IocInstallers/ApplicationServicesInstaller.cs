﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Infrastructure.MessengerServices;
using ApplicationServices.CommandHandlers;
using MacroContext.Infrastructure.CrossCuttingConcerns;
using MacroContext.ApplicationServices.CrossCuttingConcerns.CommandHandlerDecorators;
using MacroContext.ApplicationServices.CrossCuttingConcerns.QueryHandlerDecorators;

namespace Infrastructure.IocInstallers
{
    static class ApplicationServicesInstaller
    {

        public static void RegisterServices(Container _simpleContainer)
        {
            _simpleContainer.Register(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies(), Lifestyle.Transient);
            _simpleContainer.Register(typeof(IQueryHandler<,>), AppDomain.CurrentDomain.GetAssemblies(), Lifestyle.Transient);

            _simpleContainer.Register<EventStoreImpl>(Lifestyle.Scoped); 
            _simpleContainer.Register<IEventStore>(() => _simpleContainer.GetInstance<EventStoreImpl>());

            //CommandHandlerDecorators --> execution order bottom to top
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(ExceptionHandlerCommandHandlerDecorator<>));
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            //QueryHandlerDecorators --> execution order bottom to top
            _simpleContainer.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ExceptionHandlerQueryHandlerDecorator<,>));
            _simpleContainer.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LoggingQueryHandlerDecorator<,>));

        }
    }
}
