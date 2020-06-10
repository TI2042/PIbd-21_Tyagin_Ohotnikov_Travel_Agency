using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя гида")]
        public string GuideThemeName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
    }
}
