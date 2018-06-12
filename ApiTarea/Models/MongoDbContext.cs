using MongoDB.Bson;
using MongoDB.Driver;
using Examen.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Examen.Services;

namespace Examen.Models
{
    public class MongoDbContext
    {
        private MongoClient _Connection;

        private IMongoDatabase _Database;

        public MongoDbContext()
        {
            _Connection = new MongoClient();
            _Database = _Connection.GetDatabase("Pages");
        }

        public MongoDbContext(string url)
        {
            _Connection = new MongoClient("mongodb://" + url);
            _Database = _Connection.GetDatabase("Pages");
        }

        public async Task Insert(Page data)
        {
            var collection = _Database.GetCollection<Page>("Pages");
            await collection.InsertOneAsync(data);
        }

        public void Insert(List<Page> data)
        {
            if (data.Count == 0)
                return;

            var collection = _Database.GetCollection<Page>("Pages");

            collection.InsertMany(data);
        }

        public List<Page> SelectByMatchWord(string Word)
        {
            List<Page> output = new List<Page>();

            var filter = Builders<Page>.Filter.Regex("Content", new BsonRegularExpression(Word));

            return _Database.GetCollection<Page>("Pages").Find(filter).ToList<Page>();
        }

        public void Remove()
        {
            _Database.GetCollection<Page>("Pages").DeleteManyAsync(new BsonDocument());
        }
    }

}