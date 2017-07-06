using Proje.DataAccsessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.DataAccsessLayer.EntityFramework
{
    //
    public class RepositoryBase
    {
        //databaseContext tipinden nesne üret
        protected static DatabaseContext context;
        private static object _lockSync = new object();

        //yapıcı metod olusturarak newlenmesini önle
        protected RepositoryBase()
        {
            CreateContext();
        }

        //context yoksa oluştur varsa oluşturma
        public static void CreateContext()
        {
            if(context==null)
            {
                lock(_lockSync)

                {
                    if(context==null)
                    {
                        context=new DatabaseContext();
                    }
                }
            }

           
        }
    }
}
