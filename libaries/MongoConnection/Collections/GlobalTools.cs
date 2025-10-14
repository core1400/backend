using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Reflection;
using System.Text.Json;

namespace MongoConnection.Collections
{
    public static class GlobalTools<collectionType>
    {
        private static Dictionary<string, (string bsonName, PropertyInfo bsonType)[]> _bsonNameCache = new Dictionary<string, (string bsonName, PropertyInfo bsonType)[]>();
        public static UpdateDefinition<collectionType> GenericUpdate(JsonElement updateElements)
        {
            List<UpdateDefinition<collectionType>> updateDef = new List<UpdateDefinition<collectionType>>();
            UpdateDefinitionBuilder<collectionType> builder = Builders<collectionType>.Update;

            foreach (JsonProperty prop in updateElements.EnumerateObject())
            {
                Console.WriteLine(prop.Name);
                Console.WriteLine(IsPropArray(prop.Name));
                if(DocumentHasProperty(prop))
                    if(IsPropArray(prop.Name) && prop.Value.ValueKind == JsonValueKind.Array)
                        updateDef.Add(builder.Set(prop.Name, ConvertJsonType(prop.Value)));
                    
                    else if (prop.Value.ValueKind == JsonValueKind.Object)
                        try
                        {
                            UpadteAllObjectRecursion(prop, String.Empty, ref updateDef, ref builder);
                        }
                        catch (Exception e) { }
                    else updateDef.Add(builder.Set(prop.Name, ConvertJsonType(prop.Value)));
            }

            UpdateDefinition<collectionType> combined = builder.Combine(updateDef);
            return combined;
        }
        private static bool IsPropArray(string propName)
        {
            if (!_bsonNameCache.ContainsKey(nameof(collectionType)))
                InitializeBsonNameCache();
            foreach((string bsonName, PropertyInfo bsonType) in _bsonNameCache[nameof(collectionType)])
                if (bsonName == propName)
                    return bsonType.PropertyType.IsArray || (bsonType.PropertyType.IsGenericType && bsonType.PropertyType.GetGenericTypeDefinition() == typeof(List<>));
            return false;
        }
        private static bool DocumentHasProperty(JsonProperty prop)
        {
            if (!_bsonNameCache.ContainsKey(nameof(collectionType)))
                InitializeBsonNameCache();
            foreach((string bsonName, PropertyInfo bsonType) in _bsonNameCache[nameof(collectionType)])
            {
                Object? convertedProp = ConvertJsonType(prop.Value);
                if (convertedProp != null && bsonName == prop.Name && bsonType.PropertyType == convertedProp.GetType())
                    return true;
            }
            return false;
        }

        private static void InitializeBsonNameCache()
        {
            //Binding flags used for discrepencys in c# vs mongo naming conventions
            PropertyInfo[] props = typeof(collectionType).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string[] bsonElementNames = new string[props.Length];
            _bsonNameCache.TryAdd(nameof(collectionType), new (string, PropertyInfo)[props.Length]);

            int count = 0;
            foreach (PropertyInfo prop in props)
            {
                BsonElementAttribute? bsonAttr = prop.GetCustomAttribute<BsonElementAttribute>();
                string fieldName = bsonAttr != null ? bsonAttr.ElementName : prop.Name;
                _bsonNameCache[nameof(collectionType)][count++] = (fieldName,prop);
            }
        }

        //deep update for nested objects
        private static void UpadteAllObjectRecursion(JsonProperty mainProp, string path, ref List<UpdateDefinition<collectionType>> updateDef, ref UpdateDefinitionBuilder<collectionType> builder)
        {
            path += mainProp.Name + Consts.MONGO_DICTIONARY_SEPERATOR;

            // escape condition reached end of object
            if (mainProp.Value.ValueKind != JsonValueKind.Object)
            {
                path = path.Remove(path.Length - Consts.EXTRA_DOT_LENGTH); 
                updateDef.Add(builder.Set(path, ConvertJsonType(mainProp.Value)));
                return;
            }

            foreach (JsonProperty prop in mainProp.Value.EnumerateObject())
                UpadteAllObjectRecursion(prop, path, ref updateDef, ref builder);
        }

        private static Object? ConvertJsonType(JsonElement prop)
        {
            Object? value = null;
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
    }
}
