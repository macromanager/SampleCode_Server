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
    public class AddReferenceProfilesToPackageCommandHandler : ICommandHandler<AddReferenceProfilesToPackageCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public AddReferenceProfilesToPackageCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(AddReferenceProfilesToPackageCommand command)
        {
            var refProfiles = new List<ReferenceProfile>();
            foreach(var referenceProfileDto in command.ReferencesProfiles)
            {
                var referenceProfile = new ReferenceProfile(Guid.NewGuid(), referenceProfileDto.PackageId, referenceProfileDto.ReferenceId);
                referenceProfile.EditInfo(referenceProfileDto.Name, referenceProfileDto.ReferenceVersion, null);
                refProfiles.Add(referenceProfile);

                //var profile = new MacroProfile(profileDto.Id, profileDto.PackageId, macro);
                //profile.UpdateProfile(profileDto.MacroPosition, profileDto.ComponentType, profileDto.ComponentName, null);
                //profiles.Add(profile);

                _eventStore.AddToEventQueue(new ReferenceProfileAddedEvent(referenceProfile.Id));

            }

            _unitOfWork.ReferenceProfiles.AddRange(refProfiles);
            _unitOfWork.Complete();

        }

    }
}
