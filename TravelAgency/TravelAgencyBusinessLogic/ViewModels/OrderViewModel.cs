﻿using TravelAgencyBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        [DisplayName("Название тура")]
        public string TourName { get; set; }
        [DisplayName("Статус")]
        public Status Status { get; set; }
        [DisplayName("Дата создания")]
        public DateTime CreationDate { get; set; }
        [DisplayName("Дата завершения")]
        public DateTime? CompletionDate { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
    }
}
