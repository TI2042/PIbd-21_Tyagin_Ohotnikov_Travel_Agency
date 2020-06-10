using System;
using System.Collections.Generic;
using System.ComponentModel;
using TravelAgencyBusinessLogic.Enums;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class RequestViewModel
    {
        [DisplayName("Номер заявки")]
        public int Id { get; set; }
        public int SupplierId { get; set; }
        [DisplayName("ФИО")]
        public string SupplierFIO { get; set; }
        
        [DisplayName("Статус")]
        public RequestStatus Status { get; set; }
        [DisplayName("Дата ")]
        public DateTime Date { get; set; }
        public Dictionary<int,(string,int,bool)> Guides { get; set; }
    }
}
