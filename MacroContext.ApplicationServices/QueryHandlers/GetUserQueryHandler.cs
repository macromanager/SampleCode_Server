using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroContext.Persistance;
using MacroContext.Domain;
using MacroContext.Contract.Queries;
using MacroContext.Contract.Dto;
using MacroContext.Shared.Utilities;

namespace MacroContext.ApplicationServices.QueryHandlers
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserDto Handle(GetUserQuery query)
        {
            var user = _unitOfWork.Users.Get(query.UserId);
            var userDto = _mapper.Map<User, UserDto>(user);
            return userDto;

        }
    }
}
