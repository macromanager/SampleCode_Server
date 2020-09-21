using ApplicationServices.CommandHandlers;
using MacroContext.Contract.Commands;
using MacroContext.Contract.Dto;
using MacroContext.Contract.Events;
using MacroContext.Domain;
using MacroContext.Persistance;
using MacroContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.ApplicationServices.CommandHandlers
{
    public class RemoveReferenceProfilesFromPackageCommandHandler : ICommandHandler<RemoveReferenceProfilesFromPackageCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public RemoveReferenceProfilesFromPackageCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(RemoveReferenceProfilesFromPackageCommand command)
        {
            var profiles = new List<ReferenceProfile>(command.ReferenceProfiles.Count);
            foreach(var referenceProfileDto in command.ReferenceProfiles)
            {
                var referenceProfile = new ReferenceProfile(referenceProfileDto.Id, referenceProfileDto.PackageId, referenceProfileDto.ReferenceId);
                referenceProfile.EditInfo(referenceProfileDto.Name, referenceProfileDto.ReferenceVersion, referenceProfileDto.RowVersion);
                profiles.Add(referenceProfile);
                //var profileDto = completeMacroDto.MacroProfile;
                //var profile = new MacroProfile(profileDto.Id, profileDto.PackageId, macro);
                //profile.UpdateProfile(0, ComponentType.ClassModule, null, profileDto.RowVersion);
                //profiles.Add(profile);

                _eventStore.AddToEventQueue(new ReferenceProfileRemovedEvent(referenceProfile.Id));
            }
            
            _unitOfWork.ReferenceProfiles.RemoveRange(profiles);
            _unitOfWork.Complete();

        }

    }
}
