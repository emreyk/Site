using Proje.Business;
using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje.Bitirme.Controllers
{
    public class CleanerController : Controller
    {
        // GET: Cleaner
        public string CleanNotes()
        {
            NoteManager nm = new NoteManager();
            LikedManager lm = new LikedManager();
            CommentManager cm = new CommentManager();

            DateTime minDate = DateTime.Now.AddMinutes(-2);

            // 2 dk dan eski notlar bulunur.
            List<Note> toDeleteNotes = nm.List(x => x.CreatedOn < minDate && x.IsDeletedNote);

            // Notun like ve comment'leri silinir..
            toDeleteNotes.ForEach(x =>
            {
                foreach (Comment com in x.Commnets.ToList())
                {
                    cm.Delete(com);
                }

                foreach (Liked liked in x.Likes.ToList())
                {
                    lm.Delete(liked);
                }
            });

            // Notlar silinir.
            foreach (Note note in toDeleteNotes)
            {
                nm.Delete(note);
            }

            return "ok";
        }
    }
}