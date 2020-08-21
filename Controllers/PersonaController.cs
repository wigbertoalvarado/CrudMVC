using MVCCrud.Models;
using MVCCrud.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MVCCrud.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            List<Persona> lst;

            using (Crud1Entities contex = new Crud1Entities()) 
            {
                lst = (from d in contex.personas
                       select new Persona
                       {
                           Id = d.id,
                           Nombre = d.nombre,
                           Correo = d.correo,
                       }).ToList();
                        
            }

                return View(lst);
        }

        public ActionResult Nuevo() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model) 
        {
            try 
            {
                if (ModelState.IsValid) 
                {
                    using (Crud1Entities contex = new Crud1Entities()) 
                    {
                        var oTabla = new persona();

                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;

                        contex.personas.Add(oTabla);
                        contex.SaveChanges();
                    }
                    return Redirect("~/Persona");
                }
               
                return View(model);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            TablaViewModel model = new TablaViewModel();

            using (Crud1Entities contex = new Crud1Entities()) 
            {
                var oTabla = contex.personas.Find(id);
                model.Nombre = oTabla.nombre;
                model.Correo = oTabla.correo;
                model.Fecha_Nacimiento =(DateTime) oTabla.fecha_nacimiento;
                model.Id = oTabla.id;
            }

                return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Crud1Entities contex = new Crud1Entities())
                    {
                        var oTabla = contex.personas.Find(model.Id);

                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;

                        contex.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        contex.SaveChanges();
                    }
                    return Redirect("~/Persona");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            TablaViewModel model = new TablaViewModel();

            using (Crud1Entities contex = new Crud1Entities())
            {
                var oTabla = contex.personas.Find(id);
                contex.personas.Remove(oTabla);
                contex.SaveChanges();
            }

            return Redirect("~/Persona/");
        }
    }
}