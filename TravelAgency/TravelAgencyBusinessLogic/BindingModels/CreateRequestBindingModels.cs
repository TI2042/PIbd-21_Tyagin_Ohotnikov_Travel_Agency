using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class CreateRequestBindingModel
    {
        public int? SupplierId { get; set; }
        public Dictionary<int, (string, int)> Guides { get; set; }
    }
}
