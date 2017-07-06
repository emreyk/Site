using Proje.Common;
using Proje.Core.DataAccess;
using Proje.DataAccsessLayer;

using Proje.DataAccsessLayer.EntityFramework;
using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class //classdan gelebilecegini belirt
    {

        private DbSet<T> _objectSet;



        //sürekli DbSet<T> yazmaktan kurtardık
        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        //listele
        public List<T> List()
        {
            return _objectSet.ToList();
        }

        //eger gelen listeye sorgu yazmak istiyosak 
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        //istege göre sırala
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }


        //insert et
        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            //otamatik yap
            if (obj is EntityBase) //gelen nesne EntityBase ise
            {
                EntityBase o = obj as EntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUsername = App.Comman.GetCurrentUsername(); //bak işlem yapan kullanıcı adı yazılmalı

            }
            return Save();
        }

        //update
        public int Update(T obj)
        {
            if (obj is EntityBase) //gelen nesne EntityBase ise
            {
                EntityBase o = obj as EntityBase;



                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Comman.GetCurrentUsername(); //bak işlem yapan kullanıcı adı yazılmalı

            }
            return Save();
        }

        //delete
        public int Delete(T obj)
        {

            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        //tek bir tip döner
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

    }
}
