﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class GuideBindingModel
    {
        public int? Id { get; set; }
        public string GuideThemeName { get; set; }
        public decimal Price { get; set; }
    }
}
