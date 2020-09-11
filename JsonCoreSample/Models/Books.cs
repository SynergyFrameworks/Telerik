using JsonSample.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonSample.Models
{


    public class BookList
    {
        [JsonConverter(typeof(JsonModelConverter))]
        public List<Book> book { get; set; }
    }
    public class Book
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
      
        [JsonProperty("author")]
        public string author { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("category")]
        public string category { get; set; }

    }
}
