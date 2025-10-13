using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections.UserModel
{
    public class UserRepository : IUserRepository
    {
        public IMongoCollection<User> _userCollection;
        public UserRepository(MongoContext mongoContext)
        {
            _userCollection = mongoContext.GetCollection<User>(Consts.USER_DATABASE_NAME);    
        }

        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        public async Task DeleteByPNumAsync(int personalNumber)
        {
            await _userCollection.DeleteOneAsync(user => user.PersonalNumber == personalNumber);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userCollection.Find(FilterDefinition<User>.Empty).ToListAsync();
        }

        public async Task<User?> GetByPNumAsync(int personalNumber)
        {
            return await _userCollection.Find(user => user.PersonalNumber == personalNumber).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, JsonElement updateElements)
        {
            List<UpdateDefinition<User>> updateDef = new List<UpdateDefinition<User>>();
            UpdateDefinitionBuilder<User> builder = Builders<User>.Update;

            foreach (JsonProperty prop in updateElements.EnumerateObject())
                updateDef.Add(builder.Set(prop.Name, prop.Value.GetString()));

            UpdateDefinition<User> combined = builder.Combine(updateDef);
            await _userCollection.UpdateOneAsync(user => user.Id == id, combined);
        }

        public async Task UpdateByPNumAsync(int personalNumber, JsonElement updateElements)
        {
            List<UpdateDefinition<User>> updateDef = new List<UpdateDefinition<User>>();
            UpdateDefinitionBuilder<User> builder = Builders<User>.Update;

            foreach (JsonProperty prop in updateElements.EnumerateObject())
            {
                if(prop.Value.ValueKind == JsonValueKind.Object)
                {
                    //(string path,Object value) = GetObjectPath(prop);
                    UpadteAllObjectRecursion(prop, "", ref updateDef, ref builder);
                    //updateDef.Add(builder.Set(path, value));
                }
                //else updateDef.Add(builder.Set(prop.Name, GetJsonType(prop.Value)));
            }

            UpdateDefinition<User> combined = builder.Combine(updateDef);
            await _userCollection.UpdateOneAsync(user => user.PersonalNumber == personalNumber, combined);
        }

        private void UpadteAllObjectRecursion(JsonProperty mainProp,string path,ref List<UpdateDefinition<User>> updateDef,ref UpdateDefinitionBuilder<User> builder)
        {
            path +=  mainProp.Name+".";
            if (mainProp.Value.ValueKind != JsonValueKind.Object)
            {
                path = path.Remove(path.Length - 1);
                updateDef.Add(builder.Set(path, GetJsonType(mainProp.Value)));
                return;
            }
            foreach (JsonProperty prop in mainProp.Value.EnumerateObject())
                UpadteAllObjectRecursion(prop, path,ref updateDef,ref builder);
        }

        //private (string path, Object) GetObjectPath(JsonProperty prop)
        //{
        //    string path = prop.Name;
        //    JsonElement newProp = prop.Value;
        //    while (newProp.ValueKind == JsonValueKind.Object)
        //    {
        //        JsonProperty temp = newProp.EnumerateObject().First();
        //        path += "." + temp.Name;
        //        newProp = temp.Value;
        //        Console.WriteLine(path);
        //        Console.WriteLine(newProp.ToString());
        //        Console.WriteLine(newProp.GetRawText());
                
        //    }
           
        //    Console.WriteLine(newProp.GetRawText());
        //    Console.WriteLine(path);

        //    return (path, GetJsonType(newProp));
        //}


        private Object GetJsonType(JsonElement prop)
        {
            Object ? value = null;
            Console.WriteLine(  prop.ValueKind);
            switch (prop.ValueKind)
            {
                case JsonValueKind.String:
                    value = prop.GetString();
                    break;

                case JsonValueKind.Number:
                    if (prop.TryGetInt32(out var intValue))
                        value = intValue;
                    else if (prop.TryGetDouble(out var doubleValue))
                        value = doubleValue;
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    value = prop.GetBoolean();
                    break;

                case JsonValueKind.Array:
                    value = JsonSerializer.Deserialize<List<object>>(prop.GetRawText());
                    break;

                case JsonValueKind.Null:
                    value = null;
                    break;
            }
            return value;
        }
        
        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _userCollection.DeleteOneAsync(user => user.Id == id);
        }
    }
}
