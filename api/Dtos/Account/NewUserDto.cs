using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewUserDto
    {
        public String UserName { get; set; }

        public String Email { get; set; }

        public string Token { get; set; }
    }
}