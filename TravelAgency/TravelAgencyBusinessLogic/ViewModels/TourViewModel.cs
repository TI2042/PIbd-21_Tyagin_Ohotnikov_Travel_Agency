using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class TourViewModel
    {
        public int Id { get; set; }
        [DisplayName("Дата создания")]
        public DateTime CreationDate { get; set; }
        [DisplayName("Сумма")]
        public decimal Amount { get; set; }
        [DisplayName("Дата завершения")]
        public DateTime CompletionDate { get; set; }
        [DisplayName("Статус")]
        public Status Status { get; set; }
        public int Guide { get; set; }
    }
}
