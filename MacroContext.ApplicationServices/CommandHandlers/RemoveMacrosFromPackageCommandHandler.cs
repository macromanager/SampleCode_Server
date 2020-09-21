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
    public class RemoveMacrosFromPackageCommandHandler : ICommandHandler<RemoveMacrosFromPackageCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public RemoveMacrosFromPackageCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(RemoveMacrosFromPackageCommand command)
        {
            var profiles = new List<MacroProfile>(command.CompleteMacros.Count);
            foreach(var completeMacroDto in command.CompleteMacros)
            {
                var macroDto = completeMacroDto.Macro;
                var macro = new Macro(macroDto.Id);
                macro.EditInfo(null, null, null, MacroType.Declaration, null);
                var profileDto = completeMacroDto.MacroProfile;
                var profile = new MacroProfile(profileDto.Id, profileDto.PackageId, macro);
                profile.UpdateProfile(0, ComponentType.ClassModule, null, profileDto.RowVersion);
                profiles.Add(profile);
            
                _eventStore.AddToEventQueue(new MacroRemovedEvent(macroDto.Id, profileDto.Id));
            }
            
            _unitOfWork.MacroProfiles.RemoveRange(profiles);
            _unitOfWork.Complete();

        }

    }
}
