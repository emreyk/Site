using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proje.Business;
using Proje.Entities;
using System.Net;
using Proje.Entities.ValueObjects;
using System.Web.Mvc;
using Proje.Bitirme.ViewModels;
using Proje.Business.Results;
using Proje.Bitirme.Models;
using Proje.Entities.Messages;
namespace Proje.Bitirme.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager nm = new NoteManager();
        private CategoryManager cm = new CategoryManager();
        private UserManager eum = new UserManager();
        // GET: Home
        public ActionResult Index()
        {
            
            /* int page = 1;

            if (Request.QueryString["page"] != null)
            {
                page = int.Parse(Request.QueryString["page"]);
            }

            //temdata yoksa tüm notları listele
            ViewBag.TotalNoteCount = nm.ListQueryable().Count();
            List<Note> notes = nm.ListQueryable().OrderByDescending(x => x.ModifiedOn).Skip((page - 1) * 12).Take(12).ToList();

            //sondan sırala
            return View(notes);*/
             

            //temdata yoksa tüm notları listele
            

            //sondan sırala
            return View(nm.ListQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        //category ait notlar
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //id yakala
            
            Category cat = cm.Find(x=>x.Id==id.Value);


            if (cat == null)
            {
                return HttpNotFound();

            }

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }


        //en begenilenler
        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            return View("Index", nm.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());

        }

        //profile görüntüle
        public ActionResult ShowProfile()
        {
            
            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(CurrentSession.User.Id); //id li kullanıcıyı getir

            //hata varsa
            if(res.Errors.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title="Hata olustu",
                    Items=res.Errors
                };
                return View("Error",errorNotifyObj);

            }
            return View(res.Result);
        }

        //profile edit
        public ActionResult EditProfile()
        {
            
            UserManager eum = new UserManager();

            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(CurrentSession.User.Id); //id li kullanıcıyı getir

            //hata varsa
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata olustu",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);

            }
            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(EvernoteUser model)
        {
            ModelState.Remove("ModifiedUsername");
            if(ModelState.IsValid)
            {
                BusinessLayerResult<EvernoteUser> res = eum.UpdateProfile(model);


                if (res.Errors.Count > 0)
                {

                    ErrorViewModel errorNotifyObj = new ErrorViewModel()

                    {

                        Items = res.Errors,

                        Title = "Profil Güncellenemedi.",

                        RedirectingUrl = "/Home/EditProfile"

                    };

                    return View("Error", errorNotifyObj);

                }


              
                  
               // Profil güncellendiği için session güncellendi.
                CurrentSession.Set<EvernoteUser>("login", res.Result);

                return RedirectToAction("ShowProfile");
                
            }
            return View(model);
        }

        //profil sil
        public ActionResult RemoveProfile()
        {
            return View();
        }

        //login
        public ActionResult Login()
        {
            return View();
        }

        //login post
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //model geldiyse-dogruysa
            if(ModelState.IsValid)
            {
                UserManager eum = new UserManager();
                BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);

                if (res.Errors.Count > 0) //hata varsa
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message)); //hata ekle modeli dön
                    return View(model);
                }

                CurrentSession.Set<EvernoteUser>("login", res.Result); //sessiona kullanıcı bilgi saklama
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //register
        public ActionResult Register()
        {
            return View();
        }

        //register post
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //model dogruysa validation hataları yoksa
            if (ModelState.IsValid)
            {
                UserManager eum = new UserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);

                if (res.Errors.Count > 0) //hata varsa
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message)); //hata ekle modeli dön
                    return View(model);
                }

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt başarılı",
                    RedirectingUrl="/Home/Login",
                };

                notifyObj.Items.Add("Kayıt İşleminiz Başarılı");

                return View("ok",notifyObj); //hata yoksa ok sayfasına git
            }
            return View(model);
        }

      
        //çıkış
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }
    }



}