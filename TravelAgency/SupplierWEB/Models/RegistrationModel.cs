using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplierWEB.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите ФИО")]
        public string SupplierFIO { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(25, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите E-Mail")]
        [EmailAddress(ErrorMessage = "Вы ввели некорректный E-Mail")]
        public string Email { get; set; }
    }
}
