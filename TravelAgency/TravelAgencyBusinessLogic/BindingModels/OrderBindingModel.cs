using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public Status Status { get; set; }
        public int TourId { get; set; }
    }
}
