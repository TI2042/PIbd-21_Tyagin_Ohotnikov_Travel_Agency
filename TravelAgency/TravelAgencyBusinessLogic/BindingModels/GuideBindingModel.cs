using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class Guide
    {
        public int? Id { get; set; }
        public string FIO { get; set; }
        public string Country { get; set; }
        public string Hotel { get; set; }
        public int ProviderId { get; set; }
    }
}
