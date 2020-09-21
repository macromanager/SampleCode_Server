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
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public UpdateUserCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _eventStore = eventStore;
            _unitOfWork = uow;
        }


        public void Execute(UpdateUserCommand command)
        {
            var userDto = command.User;
            var user = new User(userDto.Id);

            user.EditInfo(userDto.Name, null);
            _eventStore.AddToEventQueue(new UserUpdatedEvent(userDto));
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();

        }

    }
}
