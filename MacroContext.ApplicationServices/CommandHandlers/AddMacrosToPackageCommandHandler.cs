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
    public class AddMacrosToPackageCommandHandler : ICommandHandler<AddMacrosToPackageCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public AddMacrosToPackageCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(AddMacrosToPackageCommand command)
        {
            var profiles = new List<MacroProfile>(command.CompleteMacros.Count);
            foreach(var completeMacroDto in command.CompleteMacros)
            {
                var macroDto = completeMacroDto.Macro;
                var profileDto = completeMacroDto.MacroProfile;

                var macro = new Macro(macroDto.Id);
                macro.EditInfo(macroDto.Name, macroDto.Description, macroDto.Code, macroDto.MacroType, null);

                var profile = new MacroProfile(profileDto.Id, profileDto.PackageId, macro);
                profile.UpdateProfile(profileDto.MacroPosition, profileDto.ComponentType, profileDto.ComponentName, null);
                profiles.Add(profile);

                _eventStore.AddToEventQueue(new MacroAddedEvent(macroDto.Id, profileDto.Id));

            }

            _unitOfWork.MacroProfiles.AddRange(profiles);
            _unitOfWork.Complete();

        }

    }
}
