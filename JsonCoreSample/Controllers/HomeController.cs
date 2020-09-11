using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JsonSample.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace JsonSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = GetBooks();
            return View(model.ToList());
        }



        public IActionResult Books_Read([DataSourceRequest] DataSourceRequest request)
        {
            

            DataSourceResult result = GetBooks().ToDataSourceResult(request);
            return Json(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = false });

        }

        private static IList<Book> GetBooks()
        {
            var jsonFile = "d:/JsonCoreSample/JsonCoreSample/Json/products.json";

            using (StreamReader file = System.IO.File.OpenText(jsonFile))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject JObj = (JObject)JToken.ReadFrom(reader);

                JArray bookArray = (JArray)JObj["products"];

                IList<Book> books = bookArray.ToObject<IList<Book>>();

                //    List<Book> FromJson(string json) => JsonConvert.DeserializeObject<BookList>(json, Converter.Settings).model;
                //    IList<BookList> books = JsonConvert.DeserializeObject<IList<BookList>>(json);


                return books;
            }

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
