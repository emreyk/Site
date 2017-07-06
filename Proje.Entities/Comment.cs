using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Entities
{
    [Table("Comments")]
    public class Comment:EntityBase
    {
        [Required, StringLength(300)]

        public string Text { get; set; }


        //notun yorumu vardır.Bu yüzden yorum notla ilişkilidir.
        public virtual Note Note { get; set; }

        //yorumun sahibi vardır.
        public virtual EvernoteUser Owner { get; set; }
    }
}
