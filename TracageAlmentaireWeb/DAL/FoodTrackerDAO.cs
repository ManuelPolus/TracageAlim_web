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
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.DAL
{
    public class FoodTrackerDao<T> : IRepository<T>
    {
        //TODO : trouver comment changer le nom de la table. L'intégrer aux args et raccourcir dans la business layer ?
        //TODO : eventually replace with azure sql server's url
        private const string ConnexionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False";

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
            }
        }

        public void Delete(object identifier)
        {
            using (_connexion)
            {

                _connexion.Open();

                var command = new SqlCommand(
                    "DELETE FROM RoleUtilisateur" +
                    " WHERE Id =\'" + identifier +
                    "\' FOR JSON AUTO ;"
                    , _connexion);

                command.ExecuteReader();
            }
        }

        public void Delete(object identifier, string identifierName)
        {
            using (_connexion)
            {

                _connexion.Open();

                var command = new SqlCommand(
                    "DELETE FROM RoleUtilisateur" +
                    " WHERE " + identifierName + " =\'" + identifier +
                    "\' FOR JSON AUTO ;"
                    , _connexion);

                command.ExecuteReader();
            }
        }



        public void Update(T newStateOfEntity, object entityIdentifier)
        {

            using (_connexion)
            {
                var currentStateOfEntity = GetByIdentifier(entityIdentifier);
                //If the current state and wished state are different, something is indeed different and we actually need an update
                if (!currentStateOfEntity.Equals(newStateOfEntity))
                {
                    var command = new SqlCommand(
                        "UPDATE " + typeof(T)
                        + ""
                        , _connexion);

                }

            }
        }



        public IEnumerable<T> All()
        {
            IEnumerable<T> dataList = new List<T>();
            var result = "";

            using (_connexion)
            {
                _connexion.Open();
                var command = new SqlCommand("SELECT * FROM RoleUtilisateur FOR JSON AUTO", _connexion);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        result += reader.GetName(0) + reader.GetString(0);
                    }

                    var deserializedResult = DataConverter<T>.ConvertToModels(result);

                    dataList = deserializedResult;

                }
            }
            //TODO serialization
            return dataList;
        }


        
        public T GetByIdentifier(object identifier)
        {
            T item;
            var result = "";

            using (_connexion)
            {
                _connexion.Open();
                var command = new SqlCommand("SELECT * FROM RoleUtilisateur WHERE Id =\'" + identifier + "\' FOR JSON AUTO", _connexion);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        result += reader.GetName(0) + reader.GetString(0);
                    }

                    var deserializedResult = DataConverter<T>.ConvertToModel(result);
                    item = deserializedResult;
                }

            }
            //TODO serialization
            return item;
        }

        public T GetByIdentifier(object identifier, string identifierName)
        {
            T item;
            var result = "";

            using (_connexion)
            {
                _connexion.Open();
                var command = new SqlCommand("SELECT * FROM RoleUtilisateur WHERE " + identifierName + " =\'" + identifier + "\' FOR JSON AUTO", _connexion);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        result += reader.GetName(0) + reader.GetString(0);
                    }

                    var deserializedResult = DataConverter<T>.ConvertToModel(result);
                    item = deserializedResult;
                }

            }
            //TODO serialization
            return item;
        }

    }
}

/*
 *   
 */
