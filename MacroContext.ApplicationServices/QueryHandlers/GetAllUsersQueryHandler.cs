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
using MacroContext.Shared.ValueObjects;

namespace MacroContext.ApplicationServices.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, PagedResult<UserDto>>
    {
        private IUnitOfWork _unitOfWork;
        private IMyMapper _mapper;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public PagedResult<UserDto> Handle(GetAllUsersQuery query)
        {
            //var users = _unitOfWork.Users.GetAll();
            //var userDtos = _mapper.Map<User[], UserDto[]>(users.ToArray());
            //return userDtos;

            var paging = query.Paging;
            var paged = _unitOfWork.Users.GetAll(query.Paging);
            var userDtos = _mapper.Map<User[], UserDto[]>(paged.Result);
            var resultDto = new PagedResult<UserDto>(paging, paged.PageCount, paged.ItemCount, userDtos);
            return resultDto;


        }
    }
}
