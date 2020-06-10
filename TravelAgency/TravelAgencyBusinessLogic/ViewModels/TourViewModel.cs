using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TravelAgencyBusinessLogic.ViewModels
{
    [DataContract]
    public class TourViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название тура")]
        public string TourName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> TourGuides { get; set; }
    }
}
