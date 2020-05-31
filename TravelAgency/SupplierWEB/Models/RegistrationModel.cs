using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierWEB.Models
{
    public class RegistrationModel
    {
        [DisplayName("ФИО")]
        [Required(ErrorMessage = "Пожалуйста, введите ФИО")]
        public string SupplierFIO { get; set; }
        
        [DisplayName("Электронная почта")]
        [Required(ErrorMessage = "Пожалуйста, введите E-Mail")]
        public string Login { get; set; }
        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        [StringLength(25, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string Password { get; set; }

    }
}
