﻿using Microsoft.EntityFrameworkCore;
using pis;
using pis.Repositorys;
using System.Runtime.Intrinsics.Arm;

namespace pis_web_api.Repositorys
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected delegate void ModelAction(Context db, T model);
        public Context db;
        public DbSet<T> dbSet;

        public Repository()
        {
            this.db = new Context();
            dbSet = db.Set<T>();
        }

        protected bool DoWork(T model, ModelAction modelAction)
        {
           try
           {
               modelAction(db, model);
               db.SaveChanges();
           }
           catch (Exception)
           {
               return false;
           }
           return true;
           
        }

        public bool Add(T model) =>
            DoWork(model, (db, modelT) => db.Add(modelT));
        public bool Remove(T model) =>
            DoWork(model, (db, modelT) => db.Remove(modelT));
        public bool Update(T model) =>
            DoWork(model, (db, modelT) => db.Update(modelT));

        virtual public T GetById(int id)
        {
            var model = dbSet.Find(id);
            if (model == null)
                throw new Exception();
            return model;
        }
    }
}
