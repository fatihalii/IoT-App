using HefaWebhook.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HefaWebhook.Services
{
    public class SignalsService
    {
        private readonly IMongoCollection<Signal> _signalsCollection;

        public SignalsService(IOptions<SignalsDatabaseSettings> signalsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                signalsDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                signalsDatabaseSettings.Value.DatabaseName);
             _signalsCollection = mongoDatabase.GetCollection<Signal>(
                signalsDatabaseSettings.Value.SignalsCollectionName);
        }

        public async Task<List<Signal>> GetAsync() =>
            await _signalsCollection.Find(_ => true).ToListAsync();

        public async Task<Signal?> GetAsync(string id) =>
            await _signalsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Signal newSignal) =>
            await _signalsCollection.InsertOneAsync(newSignal);

        public async Task UpdateAsync(string id, Signal updatedSignal) =>
            await _signalsCollection.ReplaceOneAsync(x => x.Id == id, updatedSignal);

        public async Task RemoveAsync(string id) =>
            await _signalsCollection.DeleteOneAsync(x => x.Id == id);

    }
}
