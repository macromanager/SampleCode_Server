using ApplicationServices.CommandHandlers;
using MacroContext.Contract.Commands;
using MacroContext.Contract.Dto;
using MacroContext.Contract.Events;
using MacroContext.Domain;
using MacroContext.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.CommandHandlers
{
    public class AddPackageCommandHandler : ICommandHandler<AddPackageCommand>
    {
        private ICommandDispatcher _commandDispatcher;
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public AddPackageCommandHandler(IUnitOfWork uow, IEventStore eventStore, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _eventStore = eventStore;
            _unitOfWork = uow;
        }


        public void Execute(AddPackageCommand command)
        {
            this.AddPackage(command.Package);

            if(command.MacrosCommands != null)
            {
                this.ExecuteMacroCommands(command.MacrosCommands);
            }

            if(command.ReferenceProfileCommands != null)
            {
                this.ExecuteReferenceCommands(command.ReferenceProfileCommands);
            }
        }

        public void AddPackage(PackageDto packageDto)
        {
            var package = new Package(packageDto.Id);

            package.EditInfo(packageDto.UserId, packageDto.Name, packageDto.Description, null);
            _eventStore.AddToEventQueue(new PackageAddedEvent(packageDto.Id));
            _unitOfWork.Packages.Add(package);
            _unitOfWork.Complete();
        }

        public void ExecuteMacroCommands(ICommand[] macroCommands)
        {
            foreach(var cmd in macroCommands)
            {
                this._commandDispatcher.Submit((dynamic)cmd);
            }
        }


        public void ExecuteReferenceCommands(ICommand[] referenceCommands)
        {
            foreach (var cmd in referenceCommands)
            {
                this._commandDispatcher.Submit((dynamic)cmd);
            }
        }


    }
}
