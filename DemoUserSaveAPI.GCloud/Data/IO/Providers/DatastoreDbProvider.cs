using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoUserSaveAPILibs.Core.Configuration;
using DemoUserSaveAPILibs.Core.Data.Entity;
using DemoUserSaveAPILibs.Core.Data.IO.Providers;
using Google.Cloud.Datastore.V1;
using Grpc.Core;

namespace DemoUserSaveAPI.GCloud.Data.IO.Providers
{
    public class DatastoreDbProvider : IDataAccessProvider
    {
        private readonly DatastoreDb _db;
        private readonly IDataEntityObjectFactory _dataEntityObjectFactory;

        public DatastoreDbProvider(IConfigurationService configurationService, IDataEntityObjectFactory dataEntityObjectFactory)
        {
            var host = configurationService.Get("DATASTOREDB_HOST");
            var project = configurationService.Get("DATASTOREDB_PROJECT");
            var port = Convert.ToInt32(configurationService.Get("DATASTOREDB_PORT"));

            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            var client = DatastoreClient.Create(channel); 

            _db = DatastoreDb.Create(project, "", client);
            _dataEntityObjectFactory = dataEntityObjectFactory;
        }

        public async Task<Guid> Insert(string into, IDataEntityObject item)
        {
            var entity = new Entity()
            {
                Key = _db.CreateKeyFactory(into).CreateIncompleteKey(),
            };

            foreach(var propKey in item.Keys())
            {
                entity[propKey] = item.Get<string>(propKey);
            }

            var keys = await _db.InsertAsync(new[] { entity }).ConfigureAwait(false);
            
            return item.Id;
        }

        public async Task<IEnumerable<IDataEntityObject>> Select(string from)
        {
            var results = await _db.RunQueryAsync(new Query(from)).ConfigureAwait(false);

            var returnData = new List<IDataEntityObject>();
            foreach (var result in results.Entities)
            {
                var entity = _dataEntityObjectFactory.Create();
                
                foreach(var prop in result.Properties)
                {
                    //TODO: Get actually type from property
                    entity.Set(prop.Key, prop.Value.StringValue);
                }
                returnData.Add(entity);
            }

            return returnData;
        }

        public async Task<Guid> Update(string into, IDataEntityObject item)
        {
            throw new NotImplementedException();
        }
    }
}
