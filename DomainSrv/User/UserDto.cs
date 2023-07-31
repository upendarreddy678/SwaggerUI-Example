using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainSrv.User
{
    public class UserDto : CreateUpdateUserDto
    {
        /// <example>TestId</example>
        public string Id { get; set; } = null!;
    }
}
