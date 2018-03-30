using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Xml;
using LinqToDB;
using Newtonsoft.Json;

namespace TracageAlmentaireWeb.DAL
{
    public class FoodTrackerDao<T> : IRepository<T>
    {
        //TODO : eventually replace with azure sql server's url
        private const string ConnexionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False";

        private SqlConnection _connexion;

        public FoodTrackerDao()
        {
            _connexion = new SqlConnection();
            _connexion.ConnectionString = ConnexionString;
        }

        public void Add(T entity)
        {
            using (_connexion)
            {
                _connexion.Open();
            }
        }

        public void Delete(T entity)
        {
            using (_connexion)
            {
                _connexion.Open();


            }
        }

        public void Update(T entity)
        {
            using (_connexion)
            {
                _connexion.Open();


            }
        }

        public IEnumerable<T> All()
        {
            List<T> dataList = new List<T>();
            var result = "";

            using (_connexion)
            {
                _connexion.Open();
                var command = new SqlCommand("SELECT * FROM RoleUtilisateur FOR JSON AUTO", _connexion);

                using (var reader = command.ExecuteReader())
                {

                    int i = 0;
                    while (reader.Read())
                    {
                        result += reader.GetName(i) + reader.GetString(i);
                        i++;
                    }

                    result = result.Substring(
                        result.IndexOf('{'),
                        result.LastIndexOf('}')+1- result.IndexOf('{')
                    );

                    var deserializedResult = JsonConvert.DeserializeObject<T>(result);
                    dataList.Add(deserializedResult);
                }
            }
            //TODO serialization
            return dataList;
        }

        public T GetByIdentifier(object identifier)
        {
            throw new NotImplementedException();
        }

    }
}

/*
 *   
 */
