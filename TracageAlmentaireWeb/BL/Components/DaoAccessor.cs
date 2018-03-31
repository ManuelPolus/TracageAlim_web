using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tracage.Models;
using System.Net.Http;
using TracageAlmentaireWeb.DAL;


namespace TracageAlimentaireXamarin.BL.Components
{
    public class DaoAccessor<T> : IDataAccessor
    {

        public FoodTrackerDao<object> Dao { get; set; }

        public object DataType { get; set; }

        public DaoAccessor(object dataType)
        {
            Dao = new FoodTrackerDao<object>();
            this.DataType = dataType;
        }

        public bool Save(object currentObject)
        {
            Dao.Add(currentObject);
            return true;
        }

        public IEnumerable<T> GetAsList<T>()
        {
           return  (IEnumerable<T>) Dao.All();
        }

        public object GetByIdentifier<T>(T identifier)
        {
            Dao.GetByIdentifier(identifier);
            return true;
        }

        public object GetByIdentifier<T>(T identifier,string identifierName)
        {
            Dao.GetByIdentifier(identifier,identifierName);
            return true;
        }

        public bool DeleteByIdentifier<T>(T identifier)
        {
            Dao.Delete(identifier);
            return true;
        }

        public bool DeleteByIdentifier<T>(T identifier,string identifierName)
        {
            //implementer multi id pour delete
            Dao.Delete(identifier);
            return true;
        }

        public void DefineType(object o)
        {
            this.DataType = o;
        }
    }
}
