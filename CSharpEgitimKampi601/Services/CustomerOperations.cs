using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            //MongoDb bağlantı isteğinde bulunuyoruz
            var connection = new MongoDbConnection();
            //Tablomuza bağlanmak için tanımlıyoruz
            var customerCollection = connection.GetCustomersCollection();

            //Ekleme için parametreleri gönderiyoruz
            var document = new BsonDocument
            {
                {"CustomerName",customer.CustomerName },
                {"CustomerSurname",customer.CustomerSurname },
                {"CustomerCity",customer.CustomerCity },
                {"CustomerBalance",customer.CustomerBalance },
                {"CustomerShoppingCount",customer.CustomerShoppingCount }
            };
            //Ekleme işlemi tamamlıyoruz
            customerCollection.InsertOne(document);
        }
    }
}
