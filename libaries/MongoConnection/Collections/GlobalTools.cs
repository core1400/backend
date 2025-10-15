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
                if (DocumentHasProperty(prop))
                {
                    if (IsPropArray(prop))
                        UpdateArray(prop,ref updateDef,ref builder);

                    else if (prop.Value.ValueKind == JsonValueKind.Object)
                        try
                        {
                            UpadteAllObjectRecursion(prop, String.Empty, ref updateDef, ref builder);
                        }
                        catch (Exception e) { }
                    else updateDef.Add(builder.Set(prop.Name, ConvertJsonType(prop.Value)));
                }
            }

            UpdateDefinition<collectionType> combined = builder.Combine(updateDef);
            return combined;
        }
        private static void UpdateArray(JsonProperty prop,ref List<UpdateDefinition<collectionType>> updateDef, ref UpdateDefinitionBuilder<collectionType> builder)
        {
            prop.Value.TryGetProperty(Consts.ARRAY_UPDATE_OPERATOR, out JsonElement opElement);
            prop.Value.TryGetProperty(Consts.ARRAY_UPDATE_VALUE, out JsonElement valueElement);

            string operation = opElement.GetString() ?? String.Empty;
            Object[]? pushItems = null;
            pushItems = JsonSerializer.Deserialize<object[]>(valueElement.GetRawText());
            
            switch (operation.ToLower())
            {
                case Consts.ARRAY_PUSH_OPERATION:
                    try
                    {
                        IEnumerable<object> convertedArray = ConvertArrayType(prop, pushItems, ref updateDef, ref builder).Cast<object>();
                        updateDef.Add(builder.PushEach(prop.Name, convertedArray));
                    }catch(Exception e) { }
                    break;
                case Consts.ARRAY_SET_OPERATION:
                    try
                    {
                        IEnumerable<object> convertedArray = ConvertArrayType(prop, pushItems, ref updateDef, ref builder).Cast<object>();
                        updateDef.Add(builder.Set(prop.Name, convertedArray));
                    }
                    catch (Exception e) { }
                    break;
            }
        }
        private static Array ConvertArrayType(JsonProperty prop,Object[]? pushItems, ref List<UpdateDefinition<collectionType>> updateDef, ref UpdateDefinitionBuilder<collectionType> builder)
        {
             if(pushItems == null || pushItems.Length == 0)
                return Array.Empty<object>();

            object? arrayType = null;
            foreach ((string bsonName,PropertyInfo bsonType) modelProp in _bsonNameCache[nameof(collectionType)])
                if(modelProp.bsonName == prop.Name)
                    arrayType = modelProp.bsonType.PropertyType.GetElementType();
            
            switch (arrayType)
            {
                case Type t when t == typeof(string):
                    string[] retString = new string[pushItems.Length];
                    for(int i = 0; i < pushItems.Length; i++)
                        retString[i] = pushItems[i].ToString() ?? String.Empty;
                    return retString;

                case Type t when t == typeof(int):
                    int[] retInt = new int[pushItems.Length];
                    for (int i = 0; i < pushItems.Length; i++)
                        retInt[i] = Int32.Parse(pushItems[i].ToString() ?? "0");
                    return retInt;
                    
                case Type t when t == typeof(double):
                    double[] retDouble = new double[pushItems.Length];
                    for (int i = 0; i < pushItems.Length; i++)
                        retDouble[i] = Double.Parse(pushItems[i].ToString() ?? "0");
                    return retDouble;
                case null:
                    break;
            }

            return (Array)pushItems.OfType<object>();
        }
        private static bool IsPropArray(JsonProperty prop)
        {
            if (!_bsonNameCache.ContainsKey(nameof(collectionType)))
                InitializeBsonNameCache();

            foreach((string bsonName, PropertyInfo bsonType) in _bsonNameCache[nameof(collectionType)])
                if (bsonName == prop.Name )
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
                if (!IsPrimitive(bsonType.PropertyType))
                    return true;
                if(convertedProp != null && bsonName == prop.Name)
                    // make sure types are compatible but ignore arrays since they are handled differently
                    if (bsonType.PropertyType.IsArray || bsonType.PropertyType == convertedProp.GetType())
                        return true;
            }
            return false;
        }
        private static bool IsPrimitive(Type type)
        {
            if (type.IsPrimitive)
                return true;

            if (type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(DateTime) ||
                type == typeof(Guid))
                return true;

            if (type.IsArray)
                return IsPrimitive(type.GetElementType()!);

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
                    if (prop.TryGetInt32(out int intValue))
                        value = intValue;
                    else if (prop.TryGetDouble(out var doubleValue))
                        value = doubleValue;
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    value = prop.GetBoolean();
                    break;

                case JsonValueKind.Object:// handled seperately, type dosent matter just not null
                case JsonValueKind.Array:
                    value = true;
                    break;

                case JsonValueKind.Null:
                    value = null;
                    break;
            }
            return value;
        }
    }
}
