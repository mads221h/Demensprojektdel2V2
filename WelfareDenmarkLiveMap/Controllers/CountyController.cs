using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WelfareDenmarkLiveMap.Models;

namespace WelfareDenmarkLiveMap.Controllers
{
    [Route("counties")]
    public class CountyController : Controller
    {
        private readonly DataContext _db;

        public CountyController(DataContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("create")]
        public IActionResult Create()
        {
            using (StreamReader r = new StreamReader("counties.json"))
            {
                string json = r.ReadToEnd();
                JObject counties = (JObject)JsonConvert.DeserializeObject<JObject>(json);
                List<County> countiesList = new List<County>();
                foreach (var x in counties)
                {
                    countiesList.Add(new County(){ CountyNo = x.Key, Name = x.Value["name"].ToString() });
                }

                _db.AddRange(countiesList);
                _db.SaveChanges();
            }

            throw new Exception("Created counties!");
        }
    }
}