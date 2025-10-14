using MongoDB.Driver;
using System.Text.Json;

namespace MongoConnection.Collections
{
    public static class GlobalTools<collectionType>
    {
        public static UpdateDefinition<collectionType> GenericUpdate(JsonElement updateElements)
        {
            List<UpdateDefinition<collectionType>> updateDef = new List<UpdateDefinition<collectionType>>();
            UpdateDefinitionBuilder<collectionType> builder = Builders<collectionType>.Update;

            foreach (JsonProperty prop in updateElements.EnumerateObject())
                if (prop.Value.ValueKind == JsonValueKind.Object)
                    try
                    {
                        UpadteAllObjectRecursion(prop, String.Empty, ref updateDef, ref builder);
                    }
                    catch (Exception e) { }
                else updateDef.Add(builder.Set(prop.Name, ConvertJsonType(prop.Value)));

            UpdateDefinition<collectionType> combined = builder.Combine(updateDef);
            return combined;
        }

        //deep update for nested objects
        private static void UpadteAllObjectRecursion(JsonProperty mainProp, string path, ref List<UpdateDefinition<collectionType>> updateDef, ref UpdateDefinitionBuilder<collectionType> builder)
        {
            path += mainProp.Name + Consts.MONGO_DICTIONARY_SEPERATOR;
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
