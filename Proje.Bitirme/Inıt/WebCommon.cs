using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proje.Common;
using Proje.Entities;
using Proje.Bitirme.Models;
namespace Proje.Bitirme.Inıt
{
    public class WebCommon:IComman
    {
        public string GetCurrentUsername()
        {
            //webde session alma
            EvernoteUser user = CurrentSession.User;

            if (user != null)
            {
                return user.Username;
            }

                //yoksa system dndür
            else
                return "system";
        }
    }
}