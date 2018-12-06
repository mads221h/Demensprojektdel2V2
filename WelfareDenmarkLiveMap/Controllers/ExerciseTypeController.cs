using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WelfareDenmarkLiveMap.Models;

namespace WelfareDenmarkLiveMap.Controllers
{
    [Route("exercisetype")]
    public class ExerciseTypeController : Controller
    {
        private readonly DataContext _db;

        public ExerciseTypeController(DataContext db)
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
            if (!ModelState.IsValid)
                return View();
            var exercises = new List<ExerciseType>
            {
                new ExerciseType { Name = "Siddende knæekstension, venstre, sværdhedsgrad 2" },
                new ExerciseType { Name = "Siddende knæekstension, højre, sværdhedsgrad 2" },
                new ExerciseType { Name = "Siddende strakt arm foran" },
                new ExerciseType { Name = "Siddende knæekstension, venstre, sværdhedsgrad 1" },
                new ExerciseType { Name = "Ryg-rotation - siddende skovhugger mod højre fod" },
                new ExerciseType { Name = "Ryg-rotation - siddende skovhugger mod venstre fod" },
                new ExerciseType { Name = "Siddene ryg-rotation med arme stræk bagud, venstre" },
                new ExerciseType { Name = "Siddene ryg-rotation med arme stræk bagud, højre" },
            };
            _db.AddRange(exercises);
            _db.SaveChanges();

            throw new Exception("Created ExerciseTypes!");
        }
    }
}