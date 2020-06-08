using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class RequestBindingModel
    {
        public int? Id { get; set; }
        public int SupplierId { get; set; }
        public string SupplierFIO { get; set; }
        public decimal Sum { get; set; }
        public RequestStatus Status { get; set; }
        public Dictionary<int, (string, int, bool)> Guides { get; set; }
    }
}
