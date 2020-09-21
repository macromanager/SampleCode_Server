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
    public class RemovePackageCommandHandler : ICommandHandler<RemovePackageCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public RemovePackageCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(RemovePackageCommand command)
        {
            var packageDto = command.Package;
            var package = new Package(packageDto.Id);
            package.EditInfo(Guid.Empty, null, null, packageDto.RowVersion);
            _eventStore.AddToEventQueue(new PackageRemovedEvent(package.Id));
            _unitOfWork.Packages.Remove(package);
            _unitOfWork.Complete();

        }

    }
}
