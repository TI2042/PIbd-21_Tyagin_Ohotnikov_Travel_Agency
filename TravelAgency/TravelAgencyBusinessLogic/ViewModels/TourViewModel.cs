using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class TourViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название тура")]
        public string TourName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> TourGuides { get; set; }
    }
}
