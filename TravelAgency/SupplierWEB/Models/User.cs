using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierWEB.Models
{
    public class User :IdentityUser
    {
        public string ClientFIO { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
