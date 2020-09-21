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
    public class DownloadPackageCommandHandler : ICommandHandler<DownloadPackageCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public DownloadPackageCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(DownloadPackageCommand command)
        {
            var packageId = command.PackageId;
            //package.EditInfo(Guid.Empty, null, null, packageDto.RowVersion);
            _eventStore.AddToEventQueue(new PackageDownloadedEvent(packageId));
            _unitOfWork.Packages.BunpPackageDownloads(packageId);
            _unitOfWork.Complete();

        }

    }
}
