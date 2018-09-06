using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var collection = new List<BsonDocument>();

            //for (int i = 0; i < 5000; i++)
            //{
                var doc = new BsonDocument()
                {
                    { "id", message.Id },
                    { "texto", message.Text },
                    { "campo3",
                        new BsonDocument {
                            { "inner3", 2 }
                        }
                    }
                    //,{ "contador", i }
                };

            //    collection.Add(doc);
            //}

            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela01");
            //col.InsertOne(doc);
            //col.InsertMany(collection);

            UserProfile user = GetProfile(message.Id);

            return $"{message.User} disse '{message.Text}' \n Id {user.Id}, teve {user.Visitas} visitas.";
            
        }

        public static UserProfile GetProfile(string id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela01");

            var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
            var res = col.Find(filtro).ToList();

            UserProfile user = new UserProfile()
            {
                Id = id,
                Visitas = res.Count()
            };

            return user;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
        }
    }
}