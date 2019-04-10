using Homework_4_08_19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework_4_08_19.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }  

        [HttpPost]
        public void AddPerson(Person p)
        {
            PersonManager pm = new PersonManager(Properties.Settings.Default.Const);
            pm.AddPerson(p); 
        }
        
        public ActionResult GetPeople()
        {
            PersonManager pm = new PersonManager(Properties.Settings.Default.Const);
            IEnumerable<Person> ppl = pm.GetPeople();
            return Json(ppl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Edit(Person p)
        {
            PersonManager pm = new PersonManager(Properties.Settings.Default.Const);
            pm.Edit(p);    
        }

        public void Delete(int id)
        {
            PersonManager pm = new PersonManager(Properties.Settings.Default.Const);
            pm.Delete(id);
        }
    }
}