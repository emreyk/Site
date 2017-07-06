using Proje.Business.Abstract;
using Proje.Business.Results;
using Proje.Entities;
using Proje.Entities.Messages;
using Proje.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proje.DataAccsessLayer.EntityFramework;

namespace Proje.Business
{
    public class UserManager:ManagerBase<EvernoteUser>
    {
       

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            EvernoteUser user = Find(x => x.Username == data.Username || x.Email == data.EMail); //email ve username varmı
            BusinessLayerResult<EvernoteUser> res= new BusinessLayerResult<EvernoteUser>();

            //kullanıcı varsa hata mesajlarını döndür
            if(user!=null)
            {
                if(user.Username==data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı Kayıtlı");
                }

                if(user.Email==data.EMail)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı");
                }
            }

                //hatalar yoksa insert yap
            else
            {
                int dbResult=Insert(new EvernoteUser() { 
                
                    Username=data.Username,
                    Email=data.EMail,
                    Password=data.Password,
                    ActivateGuid=Guid.NewGuid(),
                    
                    IsActive=true,
                    IsAdmin=false,
                   

                });

                if(dbResult>1)
                {
                    res.Result = Find(x=>x.Email==data.EMail && x.Username==data.Username);
                    //yap:aktivasyon mail
                    //aktivasyon yap
                }
            }

            return res;
        }
        
        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = Find(x => x.Username == data.Username && x.Password == data.Password);



            if (res.Result != null)
            {
                if (!res.Result.IsActive)
             {
                 res.AddError(ErrorMessageCode.UserIsNotActive,"kullanıcı aktifleştirilmemiştir");
                 res.AddError(ErrorMessageCode.CheckYourEmail, "E-posta adresinizi kontrol ediniz");
             } 
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "kullanıcı adı yada sifre uyusmuyor");
            }

            return res;
        }

        //profil güncelle
        public BusinessLayerResult<EvernoteUser> UpdateProfile(EvernoteUser data)
        {
            EvernoteUser db_user = Find(x => x.Username == data.Username || x.Email == data.Email);

            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {

                if (db_user.Username == data.Username)
                {

                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");

                }



                if (db_user.Email == data.Email)
                {

                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");

                }



                return res;

            }

            res.Result = Find(x => x.Id == data.Id);

            res.Result.Email = data.Email;

            res.Result.Name = data.Name;

            res.Result.Surname = data.Surname;

            res.Result.Password = data.Password;

            res.Result.Username = data.Username;



            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {

                res.Result.ProfileImageFilename = data.ProfileImageFilename;

            }



            if (base.Update(res.Result) == 0)
            {

                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");

            }



            return res;
        }

        //id yakala
        public BusinessLayerResult<EvernoteUser> GetUserById(int id)
        {

            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            res.Result = Find(x => x.Id == id);



            if (res.Result == null)
            {

                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");

            }



            return res;

        }
    }
}
