using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.DataAccsessLayer.EntityFramework
{
    public class DatabaseContext:DbContext
    {
        //set işlemleri ile birlikte veritabanı oluşur
        public DbSet<EvernoteUser>EvernoteUser { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }


        public DatabaseContext()
        {
            //MyInitializer classındaki degerleri ekle
            Database.SetInitializer(new MyInitializer());

        }
    }
}
