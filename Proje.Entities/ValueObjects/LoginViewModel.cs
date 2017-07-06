using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proje.Entities.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("kullanıcı adı"),Required(ErrorMessage="{0} alanı boş geçilmez")]
        public string  Username { get; set; }

        [DisplayName("sifre "), Required(ErrorMessage = "{0} alanı boş geçilmez"),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}