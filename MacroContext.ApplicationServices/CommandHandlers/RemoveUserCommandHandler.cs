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
    public class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public RemoveUserCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _unitOfWork = uow;
            _eventStore = eventStore;
        }


        public void Execute(RemoveUserCommand command)
        {
            var userDto = command.User;
            var user = new User(userDto.Id);
            user.EditInfo(null, userDto.RowVersion);
            _eventStore.AddToEventQueue(new PackageRemovedEvent(command.User.Id));
            _unitOfWork.Users.Remove(user);
            _unitOfWork.Complete();

        }

    }
}
