using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class Administrator
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
