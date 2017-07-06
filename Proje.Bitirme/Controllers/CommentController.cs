using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proje.Entities;
using Proje.Business;
using System.Net;
using Proje.Bitirme.Models;

namespace Proje.Bitirme.Controllers
{
    public class CommentController : Controller
    {

        private NoteManager noteManager = new NoteManager();
        private CommentManager commentManager = new CommentManager();
        

        public ActionResult Index()
        {
            return View();
        }

        //notları göster
        public ActionResult ShowNoteComments(int? id)
        {
            //id degeri bos gelirse
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            //note degeri id dwgerini cek
            Note note = noteManager.Find(x => x.Id == id);

            
            if (note == null)
            {

                return HttpNotFound();
            }

            //nottaki yorumları retun yap
            return PartialView("_PartialComments", note.Commnets);

        }

        //update işlemini yap
        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Comment comment = commentManager.Find(x => x.Id == id);
            if (comment == null)
            {

                return new HttpNotFoundResult();

            }

            comment.Text = text;
            if (commentManager.Update(comment) > 0)
            {

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int? id)
        {
            //yoruma ait id yoksa hata dondur

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //gelen yorumu bul
            Comment comment = commentManager.Find(x => x.Id == id);

            //note yoksa
            if (comment == null)
            {

                return new HttpNotFoundResult();
            }

            if (commentManager.Delete(comment) > 0)
            {

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);

        }

         
        [HttpPost]
        public ActionResult Create(Comment comment, int? noteid)

        {
            //sil
            ModelState.Remove("CreatedOn");

            ModelState.Remove("ModifiedOn");

            ModelState.Remove("ModifiedUsername");

             //model geliyosa
            if (ModelState.IsValid)

            {
                if (noteid == null)

                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                //yorum hangi kategoride
                Note note = noteManager.Find(x => x.Id == noteid);

                if (note == null)

                {
                    return new HttpNotFoundResult();
                }


                //yorum hangi notun
                comment.Note = note;

                comment.Owner = CurrentSession.User; //sahibi suainli login olan user

                //comment insert basarılıysa true dön
                if (commentManager.Insert(comment) > 0)

                {

                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);

                }

            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);

        }

    }

}

    
