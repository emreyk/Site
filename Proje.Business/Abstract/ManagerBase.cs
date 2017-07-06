using Proje.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Abstract
{
    //newlenemez
    public abstract class ManagerBase<T>:IDataAccess<T> where T:class
    {
        private Repository<T> repo=new Repository<T>();

        public List<T> List()
        {
            //listele
            return repo.List();
        }

        public IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public List<T> List(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public int Insert(T obj)
        {
            //gelen nesneyi insert
            return repo.Insert(obj);
        }

        public int Update(T obj)
        {
            return repo.Update(obj);
        }

        public int Delete(T obj)
        {
            //gelen nesneyi al ve delete
            return repo.Delete(obj);
        }

        public int Save()
        {
            return repo.Save();
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            //gelen nesneyi kosulla
            return repo.Find(where);
        }
    }
}
