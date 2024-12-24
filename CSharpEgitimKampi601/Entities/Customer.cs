using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        //bu iki satır ile CustomerId benzersiz bir alana dönüştürüyoruz PrimaryKey gibi
        public string CustomerId { get; set; } //Idler string olarak tutuluyor
        public string CustomerName { get; set; } 
        public string CustomerSurname { get; set; } 
        public string CustomerCity { get; set; } 
        public decimal CustomerBalance { get; set; } 
        public int CustomerShoppingCount { get; set; } 

    }
}
