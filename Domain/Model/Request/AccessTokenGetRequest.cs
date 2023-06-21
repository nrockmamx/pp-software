using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class AccessTokenGetRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
