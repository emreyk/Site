using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proje.Bitirme.Models;
using Proje.Entities;
using Proje.Business;
using Proje.Entities.Messages;

namespace Proje.Bitirme.Controllers
{
    public class NoteController : Controller
    {

        NoteManager noteManager = new NoteManager();
        CategoryManager categoryManager = new CategoryManager();
        LikedManager likedManager = new LikedManager();

        public ActionResult Index()
        {
            //user ait notları al

            //notları cek join category ve owner yap where ile ksul belirti listelet
            var notes = noteManager.ListQueryable().Include("Category").Where(

             x => x.Owner.Id == CurrentSession.User.Id).OrderByDescending(

             x => x.ModifiedOn);



            return View(notes.ToList());

        }

        

        public ActionResult MyLikedNotes()
        {

            var notes = likedManager.ListQueryable().Include("LikedUser").Include("Note").Where(

                x => x.LikedUser.Id == CurrentSession.User.Id).Select(

                x => x.Note).Include("Category").Include("Owner").OrderByDescending(

                x => x.ModifiedOn);



            return View("Index", notes.ToList());

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);

            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }


        public ActionResult Create()
        {


            //note eklerken hangi kategoriye eklemek icin category list cek
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {

            //bunlar data accsess katmanında doldurur bırda tekrar isterse 
            //modelstate valid olmaz
            ModelState.Remove("CreadetOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            note.IsDeletedNote = false;

            //model dogruysa insert et
            if (ModelState.IsValid)
            {
                note.Owner = CurrentSession.User;
                noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            //hata varsa aynı sayfaya geri dön ve dropdown category ile doldur
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Create2()
        {


            //note eklerken hangi kategoriye eklemek icin category list cek
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(Note note)
        {

            //bunlar data accsess katmanında doldurur bırda tekrar isterse 
            //modelstate valid olmaz
            ModelState.Remove("CreadetOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            note.IsDeletedNote = true;

            //model dogruysa insert et
            if (ModelState.IsValid)
            {
                note.Owner = CurrentSession.User;
                noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            //hata varsa aynı sayfaya geri dön ve dropdown category ile doldur
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title", note.CategoryId);
            return View(note);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title", note.CategoryId);
            return View(note);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            //bunlar data accsess katmanında doldurur bırda tekrar isterse 
            //modelstate valid olmaz
            ModelState.Remove("CreadetOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                //update olacak degerleri sec update et
                Note db_not = noteManager.Find(x => x.Id == note.Id);
                db_not.IsDraft = note.IsDraft;
                db_not.CategoryId = note.CategoryId;
                db_not.Text = note.Text;
                db_not.Title = note.Title;

                noteManager.Update(note);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteManager.Find(x => x.Id == id);
            noteManager.Delete(note);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {
            if (CurrentSession.User != null)
            {
                //kullanıcının begendiği notların id lerini döndür
                List<int> likedNoteIds = likedManager.List(

                x => x.LikedUser.Id == CurrentSession.User.Id && ids.Contains(x.Note.Id)).Select(x => x.Note.Id).ToList();//şuan login olan kullanıcının likelar
                //Conations--> o deger iceriyo bu notun id si


                return Json(new { result = likedNoteIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }
        }

        [HttpPost]
        public ActionResult SetLikeState(int noteid, bool liked)
        {

            if(CurrentSession.User==null)
            {
                return null;
            }

            //tru ve false olmadıgını anlamak icin
            int res = 0;


            //böyle bir like varmı.Kullanıcı like yapmışsa kayıt gelir
            Liked like = likedManager.Find(x => x.Note.Id == noteid && x.LikedUser.Id == CurrentSession.User.Id);

            //notu al
            Note note = noteManager.Find(x => x.Id == noteid);

            //begenilmişse  false gelirse unlike yapılabilir
            if (like != null && liked == false)
            {
                //unlike yap
                res = likedManager.Delete(like);

            }
            
                //kulanıcı like yapıyo
            else if (like == null && liked == true)
            {
                
                res = likedManager.Insert(new Liked()

                {
                    //begenen kişi suanki kullanıcı
                    LikedUser = CurrentSession.User,

                    Note = note

                });

            }

            //bir işlem yapıldıysa
            if (res > 0)
            {
                //like yapıldıysa bir arttır
                if (liked)
                {

                    note.LikeCount++;

                }

                    //else ise unlike yapılmısştır
                else
                {

                    note.LikeCount--;

                }
                //notu update et
                res = noteManager.Update(note);

                return Json(new { hasError = false, errorMessage = string.Empty, result = note.LikeCount });

            }

            return Json(new { hasError = true, errorMessage = "zaten begendiniz.", result = note.LikeCount });

        }

        public ActionResult GetNoteText(int? id)
        {

            //gelen id null ise hata
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Note note = noteManager.Find(x => x.Id == id);

            if (note == null)
            {

                return HttpNotFound();

            }

            return PartialView("_PartialNoteText", note);

        }
    }
}
