using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmarkLiveMap.Models;

namespace WelfareDenmarkLiveMap.Controllers
{
    [Route("session")]
    public class SessionController : Controller
    {
        private readonly DataContext _db;

        public SessionController(DataContext db)
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
            
            //_db.Database.ExecuteSqlCommand("ALTER TABLE [Exercise] DROP CONSTRAINT FK_Exercise_Session_SessionID");
            //_db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Session]");
            //_db.Database.ExecuteSqlCommand("ALTER TABLE [Exercise] ADD CONSTRAINT FK_Exercise_Session_SessionID FOREIGN KEY ([SessionID]) REFERENCES [dbo].[Session] ([ID])");
            var patients = _db.Patients.ToList();
            Random random = new Random();
            var Sessions = new List<Session>();
            
            



            foreach (var Patient in patients)
            {
                for (int i = 0; i < random.Next(1, 5); i++)
                {
                    DateTime dateTime = new DateTime(random.Next(2016,2018), random.Next(1,12), random.Next(1,28));
                    var session = new Session
                    {
                        CompletionRate = random.Next(10, 100),
                        Time = dateTime,
                        Patient = Patient,

                    };

                    Sessions.Add(session);
                }
            };
            
            _db.AddRange(Sessions);
            _db.SaveChanges();
            return View();
        }
    }
}