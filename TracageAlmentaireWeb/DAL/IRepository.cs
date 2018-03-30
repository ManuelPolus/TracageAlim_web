using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracageAlmentaireWeb.DAL
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> All();

    }
}