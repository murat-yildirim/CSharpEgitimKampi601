using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class MongoDbConnection
    {
        //Field örnekliyoruz
        private IMongoDatabase _database;

        //Constructor yapıcı metot oluşturuyoruz
        public MongoDbConnection()
        {
            //bir client oluşturuyoruz bağlantının sağlanacağı adresi tutacak
            var client = new MongoClient("mongodb://localhost:27017");
            //Oluşturduğumuz Field'a Oluşturacağımız veritabanı ismini atıyoruz
            _database = client.GetDatabase("Db601Customer");
        }

        public IMongoCollection<BsonDocument> GetCustomersCollection()
        {
            //Customer adında bir koleksiyona dönüştürüyoruz yani tablo oluşturuyoruz
            return _database.GetCollection<BsonDocument>("Customers");
        }
    }
}
