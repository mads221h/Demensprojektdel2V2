using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WelfareDenmarkLiveMap.Models;

namespace WelfareDenmarkLiveMap.Controllers
{
    [Route("map")]
    public class MapController : Controller
    {
        private readonly DataContext _db;

        public MapController(DataContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("Data")]
        public JsonResult Data(string countyID)
        {
            
            Dictionary<string, object> returns = new Dictionary<string, object>();
            //Kommune-navn
            //Antal patienter
            //Gennemsnit af colpetion-rate i alle sessions
            var county = _db.County.FirstOrDefault(c => c.CountyNo == countyID);
            returns.Add("Name", county.Name);
            var patients = _db.Patients.Where(p => p.County == county);
            returns.Add("Patient-count", patients.Count());
            var sessions = _db.Session.Where(s => patients.Contains(s.Patient));
            returns.Add("Completion-avg", sessions.Average(s => s.CompletionRate));
            returns.Add("Sessions", sessions);

            return Json(returns);
        }
    }
}