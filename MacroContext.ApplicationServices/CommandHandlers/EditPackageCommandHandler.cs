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
    public class EditPackageCommandHandler : ICommandHandler<EditPackageCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;
        private ICommandDispatcher _commandDispatcher;

        public EditPackageCommandHandler(IUnitOfWork uow, IEventStore eventStore, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(EditPackageCommand command)
        {
            if(command.Package != null)
            {
                this.AddPackage(command.Package);
            }
            if (command.MacroCommands != null)
            {
                this.AddMacroCommands(command.MacroCommands);
            }
            if(command.ReferenceProfileCommands != null)
            {
                this.AddReferenceCommands(command.ReferenceProfileCommands);
            }

        }

        public void AddPackage(PackageDto packageDto)
        {
            var package = new Package(packageDto.Id);

            package.EditInfo(packageDto.UserId, packageDto.Name, packageDto.Description, packageDto.RowVersion);
            _eventStore.AddToEventQueue(new PackageEditedEvent(packageDto.Id));
            _unitOfWork.Packages.Update(package);
            _unitOfWork.Complete();

        }

        public void AddMacroCommands(ICommand[] macrosCommands)
        {
            foreach(var cmd in macrosCommands)
            {
                this._commandDispatcher.Submit((dynamic)cmd);
            }
        }

        public void AddReferenceCommands(ICommand[] referenceCommands)
        {
            foreach (var cmd in referenceCommands)
            {
                this._commandDispatcher.Submit((dynamic)cmd);
            }
        }

    }
}
