using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Contract.Dto
{
    public class UserDto
    {
        public UserDto() { }
        public UserDto(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
