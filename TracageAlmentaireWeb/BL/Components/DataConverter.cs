using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using Newtonsoft.Json;

namespace TracageAlmentaireWeb.Models
{
    public class DataConverter<T>
    {
        public static T ConvertToModel(string jsonResult)
        {
            T deserializedResult = default(T);
            try
            {
                jsonResult = FormatRawResult(jsonResult,'{','}');
                deserializedResult = JsonConvert.DeserializeObject<T>(jsonResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return deserializedResult;
        }

        public static IEnumerable<T> ConvertToModels(string jsonResult)
        {
            IEnumerable<T> deserializedResult = new List<T>();
            try
            {
                jsonResult = FormatRawResult(jsonResult,'[',']');

                deserializedResult = JsonConvert.DeserializeObject<List<T>>(jsonResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return deserializedResult;

        }

        public static List<string> GetObjectproperties(T theObject)
        {
            List<string> properties = new List<string>();

            foreach (PropertyInfo property in theObject.GetType().GetProperties())
            {
                properties.Add(property.Name);
            }

            return properties;
        }

        public static Dictionary<string, object> GetObjectpropertiesAndValues(T theObject)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
           
            foreach (PropertyInfo property in theObject.GetType().GetProperties())
            {
                properties.Add(property.Name, theObject.GetType().GetProperty(property.Name));
            }

            return properties;
        }


        private static string FormatRawResult(string stringToFormat,char beginningChar,char endingChar)
        {
            return stringToFormat.Substring(
                stringToFormat.IndexOf(beginningChar),
                stringToFormat.LastIndexOf(endingChar) + 1 - stringToFormat.IndexOf(beginningChar)
            );
        }

    }
}