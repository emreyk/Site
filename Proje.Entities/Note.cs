using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Entities
{
    [Table("Notes")]
    public class Note:EntityBase
    {
        [DisplayName("Not Başlığı"), Required, StringLength(60)]

        public string Title { get; set; }



        [DisplayName("Not Metni"), Required, StringLength(2000)]

        public string Text { get; set; }



        [DisplayName("Taslak")]

        public bool IsDraft { get; set; }



        [DisplayName("Beğenilme")]

        public int LikeCount { get; set; }


        [DisplayName("Silinebilir mi?")]
        public bool IsDeletedNote { get; set; }



        [DisplayName("Kategori")]

        public int CategoryId { get; set; }


        //notun sahibi
        public virtual EvernoteUser Owner { get; set; }

        //notun kategorisi

        public virtual Category Category { get; set; }
        
        //Notun yorumu
        public virtual List<Comment> Commnets { get; set; }

        //Notun begenileri
        public virtual List<Liked> Likes { get; set; }





        public Note()
        {

            Commnets = new List<Comment>();

            Likes = new List<Liked>();

        }
    }
}
