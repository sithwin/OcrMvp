using MongoDB.Driver;
using OcrService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcrService.Data
{
    public class MongoContext
    {
        public IMongoDatabase mongoDatabase;

        public MongoContext()
        {
            var mongoCleint = new MongoClient("mongodb://localhost:27017");
            mongoDatabase = mongoCleint.GetDatabase("OcrDB");
        }

        public IMongoCollection<PolicyInfo> PolicyInfo
        {
            get
            {
                return mongoDatabase.GetCollection<PolicyInfo>("PolicyInfo");
            }
        }
    }
}
