using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JsonSample.Utilities
{
    public class JsonModelConverter 
    {
        public object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            System.Collections.IList list = Activator.CreateInstance(objectType) as System.Collections.IList;
            Type itemType = objectType.GenericTypeArguments[0];
            foreach (JToken child in token.Children())  //mod here
            {
                object newObject = Activator.CreateInstance(itemType);
                serializer.Populate(child.CreateReader(), newObject); //mod here
                list.Add(newObject);
            }
            return list;
        }

        public bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(List<>));
        }
        public bool CanWrite => false;
        public void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
