using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Entities
{

    [Table("Likes")]

    public class Liked
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }


        //notun beğenisi 
        public virtual Note Note { get; set; }

        //kimin beğendiği
        public virtual EvernoteUser LikedUser { get; set; }

    }
    
}
