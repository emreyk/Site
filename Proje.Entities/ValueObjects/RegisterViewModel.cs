using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proje.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("kullanıcı adı"), Required(ErrorMessage = "kullanıcı adı alanı boş geçilmez")]
        public string Username { get; set; }

         [DisplayName("E-posta"), Required(ErrorMessage = "email alanı boş geçilmez"),EmailAddress(ErrorMessage= "gecerli bir e-posta adresi giriniz")]
        public string EMail { get; set; }


        [DisplayName("sifre "), Required(ErrorMessage = "sifre alanı boş geçilmez"), DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("sifre tekrar"), Required(ErrorMessage = "sifre tekrar alanı boş geçilmez"), DataType(DataType.Password),Compare("Password",ErrorMessage="şifreler uyuşmuyor")]
        public string RePassword { get; set; }
    }
}