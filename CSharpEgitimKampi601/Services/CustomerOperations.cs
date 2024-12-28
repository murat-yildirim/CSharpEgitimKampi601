using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
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

		public List<Customer> GetAllCustomer()
		{
			//MongoDb bağlantı isteğinde bulunuyoruz
			var connection = new MongoDbConnection();
			//Tablomuza bağlanmak için tanımlıyoruz
			var customerCollection = connection.GetCustomersCollection();
			//CustomerCollection içindeki verileri customers hafızasına alıyoruz
			var customers = customerCollection.Find(new BsonDocument()).ToList();
			//Bellekte boş customer listesi oluşturuyoruz
			List<Customer> customerList = new List<Customer>();
			//oluşturmuş olduğumuz boş listenin içerisine customers'taki verileri ekliyoruz.
			foreach (var c in customers)
			{
				customerList.Add(new Customer
				{
					CustomerId = c["_id"].ToString(),
					CustomerBalance = decimal.Parse(c["CustomerBalance"].ToString()),
					CustomerCity = c["CustomerCity"].ToString(),
					CustomerName = c["CustomerName"].ToString(),
					CustomerShoppingCount = int.Parse(c["CustomerShoppingCount"].ToString()),
					CustomerSurname = c["CustomerSurname"].ToString()
				});
			}
			return customerList;
		}

		public void DeleteCustomer(string id)
		{
			//MongoDb bağlantı isteğinde bulunuyoruz
			var connection = new MongoDbConnection();
			//Tablomuza bağlanmak için tanımlıyoruz
			var customerCollection = connection.GetCustomersCollection();
			//Silinecek verinin ıd'sini bulma işlemi yapılıyor
			var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
			//Bulunan id için silme işlemi gerçekleşiyor
			customerCollection.DeleteOne(filter);
		}

		public void UpdateCustomer(Customer customer)
		{
			//MongoDb bağlantı isteğinde bulunuyoruz
			var connection = new MongoDbConnection();
			//Tablomuza bağlanmak için tanımlıyoruz
			var customerCollection = connection.GetCustomersCollection();
			//verilen id değerlerini çekip hafızasına alıyor
			var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
			//oluşturulan filtreye göre hepsini düzenliyor
			var updatedValue = Builders<BsonDocument>.Update
				.Set("CustomerName", customer.CustomerName)
				.Set("CustomerSurname", customer.CustomerSurname)
				.Set("CustomerCity", customer.CustomerCity)
				.Set("CustomerBalance", customer.CustomerBalance)
				.Set("CustomerShoppingCount", customer.CustomerShoppingCount);
			//Güncelleme işlemini tamamlıyor
			customerCollection.UpdateOne(filter, updatedValue);
		}

		public Customer GetCustomerById(string id)
		{
			//MongoDb bağlantı isteğinde bulunuyoruz
			var connection = new MongoDbConnection();
			//Tablomuza bağlanmak için tanımlıyoruz
			var customerCollection = connection.GetCustomersCollection();
			//Id'ye göre filtreleme yapıyoruz
			var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
			//Tek veri getireceğimiz için firstordefault kullandık 
			var result = customerCollection.Find(filter).FirstOrDefault();
			return new Customer
			{
				CustomerBalance = decimal.Parse(result["CustomerBalance"].ToString()),
				CustomerCity = result["CustomerCity"].ToString(),
				CustomerId = id,
				CustomerName = result["CustomerName"].ToString(),
				CustomerShoppingCount = int.Parse(result["CustomerShoppingCount"].ToString()),
				CustomerSurname = result["CustomerSurname"].ToString()
			};
		}


	}
}