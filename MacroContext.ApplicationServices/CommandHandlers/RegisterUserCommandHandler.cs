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
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IEventStore _eventStore;

        public RegisterUserCommandHandler(IUnitOfWork uow, IEventStore eventStore)
        {
            _eventStore = eventStore;
            _unitOfWork = uow;
        }


        public void Execute(RegisterUserCommand command)
        {
            var userDto = command.User;
            var user = new User(userDto.Id);

            user.EditInfo(userDto.Name, null);
            _eventStore.AddToEventQueue(new UserRegisteredEvent(userDto.Id));
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();

        }

    }
}
