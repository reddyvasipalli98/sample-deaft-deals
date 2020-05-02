using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using sampledraft.Models;

namespace sampledraft.DAL
{
    public class Dal
    {
        private string userName = "a2b58212-0ee0-4-231-b9ee";
        private string host = "a2b58212-0ee0-4-231-b9ee.documents.azure.com";
        private string password = "L2qhdxWoaFoEAZlfCsRfKQmjwgEUwxhlquPl82OaMZo3BUNMEBj6P03f2qjJ8iFZ1MPpZj0EWvl7hgODeBpq0w==";
        private string dbName = "ToDoList";
        private string collectionName = "Sample";


        // Gets all Task items from the MongoDB server. 
        public List<Deal> GetAllTasks()
        {
            try
            {
                var collection = GetTasksCollection();
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (MongoConnectionException)
            {
                return new List<Deal>();
            }
        }

        // Creates a Task and inserts it into the collection in MongoDB.
        public void CreateTask(Deal deal)
        {
            var collection = GetTasksCollectionForEdit();
            try
            {
                collection.InsertOne(deal);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }
        }

        private IMongoCollection<Deal> GetTasksCollection()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<Deal>(collectionName);
            return todoTaskCollection;
        }

        private IMongoCollection<Deal> GetTasksCollectionForEdit()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<Deal>(collectionName);
            return todoTaskCollection;
        }
    }
}
