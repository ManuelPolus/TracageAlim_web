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
using LinqToDB.SqlQuery;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using TracageAlmentaireWeb.Controllers;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.DAL
{
    public class FoodTrackerDao<T> : IRepository<T>
    {
        //TODO : eventually replace with azure sql server's url

        private SqlConnection _connexion;
        private TrackingDataContext context;
        private string table;

        public FoodTrackerDao(string tableName)
        {
            table = tableName;
        }

        /// <summary>
        /// Save an object to database
        ///  </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {
                _connexion.Open();
                List<string> properties = DataConverter<T>.GetObjectproperties(entity);
                string propertiesName = "(";
                string newValues = "VALUES (";
                foreach (string property in properties)
                {
                    if (property != "Id")
                    {
                        propertiesName += property + ",";
                        newValues += "'" + entity.GetType().GetProperty(property).GetValue(entity) + "',";
                    }
                }

                propertiesName = propertiesName.Substring(0, propertiesName.Length - 1);
                propertiesName += ")";
                newValues = newValues.Substring(0, newValues.Length - 1);
                newValues += ")";

                var command = new SqlCommand(
                    "INSERT INTO " + table +
                    propertiesName +
                    newValues
                    , _connexion);

                command.ExecuteReader();
            }
        }

        /// <summary>
        /// Update an object with matching Id. If the object have a diffrent primary key than "Id",
        ///  use the overload of this method and precise the primary key attribute's name.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public virtual void Update(T newStateOfEntity, object entityIdentifier)
        {
            var currentStateOfEntity = GetByIdentifier(entityIdentifier);

            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {
                _connexion.Open();

                if (!currentStateOfEntity.Equals(newStateOfEntity))
                {
                    List<string> properties = DataConverter<T>.GetObjectproperties(newStateOfEntity);
                    string queryBody = "SET ";

                    foreach (string property in properties)
                    {
                        if (property != "Id")
                        {
                            queryBody += property + " = '" + newStateOfEntity.GetType().GetProperty(property).GetValue(newStateOfEntity) + "',";
                        }
                    }

                    queryBody = queryBody.Substring(0, queryBody.Length - 1);

                    var command = new SqlCommand(
                        "UPDATE " + table + " "
                        + queryBody
                        + " WHERE Id = '" + entityIdentifier + "'"
                        , _connexion);

                    command.ExecuteReader();
                }

            }
        }

        public virtual void Update(T newStateOfEntity, object entityIdentifier,string identifierName)
        {
            var currentStateOfEntity = GetByIdentifier(entityIdentifier);

            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {
                _connexion.Open();

                if (!currentStateOfEntity.Equals(newStateOfEntity))
                {
                    List<string> properties = DataConverter<T>.GetObjectproperties(newStateOfEntity);
                    string queryBody = "SET ";

                    foreach (string property in properties)
                    {
                        if (property != "Id")
                        {
                            queryBody += property + " = '" + newStateOfEntity.GetType().GetProperty(property).GetValue(newStateOfEntity) + "',";
                        }
                    }

                    queryBody = queryBody.Substring(0, queryBody.Length - 1);

                    var command = new SqlCommand(
                        "UPDATE " + table + " "
                        + queryBody
                        + " WHERE "+identifierName+" = '" + entityIdentifier + "'"
                        , _connexion);

                    command.ExecuteReader();
                }

            }
        }



        /// <summary>
        /// Get all the rows from a table as objects. If you wish to filter the result,
        ///  use the overload of this method by precising the property and the value you want to use.
        /// </summary>
        /// <returns>A list of objects from the database's content</returns>
        public virtual IEnumerable<T> Get()
        {
            IEnumerable<T> dataList = new List<T>();
            var result = "";

            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {
                _connexion.Open();

                var command = new SqlCommand("SELECT * FROM " + table + " FOR JSON AUTO", _connexion);

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

        public virtual IEnumerable<T> Get(string property, object value)
        {
            IEnumerable<T> dataList = new List<T>();
            var result = "";

            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {
                _connexion.Open();

                var command = new SqlCommand("SELECT * FROM " + table + " FOR JSON AUTO", _connexion);

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



        /// <summary>
        /// Get an object by it's Id. If the object have a diffrent primary key than "Id",
        ///  use the overload of this method and precise the primary key attribute's name.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public virtual T GetByIdentifier(object identifier)
        {
            T item;
            var result = "";

            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {
                _connexion.Open();
                var command = new SqlCommand("SELECT * FROM " + table + " WHERE Id =\'" + identifier + "\' FOR JSON AUTO", _connexion);

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

        public virtual T GetByIdentifier(object identifier, string identifierName)
        {
            T item;
            var result = "";

            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {
                _connexion.Open();
                var command = new SqlCommand("SELECT top 1 * FROM " + table + " WHERE " + identifierName + " =\'" + identifier + "\' FOR JSON AUTO", _connexion);

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



        /// <summary>
        /// Delete an object by it's Id. If the object have a diffrent primary key than "Id",
        ///  use the overload of this method and precise the primary key attribute's name.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public void Delete(object identifier)
        {
            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {

                _connexion.Open();

                var command = new SqlCommand(
                    "DELETE FROM " + table +
                    " WHERE Id =\'" + identifier + "\';"
                    , _connexion);

                command.ExecuteReader();
            }
        }

        public void Delete(object identifier, string identifierName)
        {
            using (_connexion = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodTracker;Integrated Security=True;Pooling=False"))
            {

                _connexion.Open();

                var command = new SqlCommand(
                    "DELETE FROM " + table +
                    " WHERE " + identifierName + " =\'" + identifier + "\';"
                    , _connexion);

                command.ExecuteReader();
            }
        }

    }
}

/*
 *   
 */
