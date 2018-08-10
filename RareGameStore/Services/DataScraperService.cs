using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RareGameStore.Services
    
{ /*
    public class DataScraperService
    {
        public static void Scrape()
        {
            string serviceURL = "https://api-endpoint.igdb.com/games/?limit=100";
            WebRequest req = WebRequest.Create(serviceURL);
            req.Headers.Add("user-key:991b2dfd1196f81497853eeb9baa4692");
            req.Headers.Add("Accept:application/json");
            WebResponse response = req.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            JsonTextReader jsonTextReader = new JsonTextReader(sr);
            JsonSerializer serializer = new JsonSerializer();

            //DataKickObject[] result = serializer.Deserialize<DataKickObject[]>(jsonTextReader);

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Ahmed;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var formats = result.Select(x => x.format).Distinct();
            foreach (var format in formats)
            {
                var scalarCommand = connection.CreateCommand();
                scalarCommand.CommandText = "SELECT COUNT(*) FROM Categories WHERE Name = '" + format + "'";
                int categoryCount = (int)scalarCommand.ExecuteScalar();
                if (categoryCount == 1)
                {
                    var categoryCommand = connection.CreateCommand();
                    categoryCommand.CommandText = "INSERT INTO Categories(Name) VALUES ('" + format + "')";
                }
            }

            foreach (var datakickObject in result)
            {
                var command = connection.CreateCommand();

            }
        }
    } */
}
