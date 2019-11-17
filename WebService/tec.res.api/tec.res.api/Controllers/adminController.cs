using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using tec.res.api.Models;

namespace tec.res.api.Controllers
{
    public class adminController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // Método para logear un nuevo ingeniero
        
        [HttpPost]
        public IHttpActionResult PostLogin(admin login)
        {
            if ((login.usuario == null) | (login.contrasena == null))
            {
                return BadRequest();
            }
            var user = from i in db.admin
                       where i.usuario == login.usuario
                       select new { i.contrasena };

            if (user.Count() == 0)
            {
                return Content(HttpStatusCode.NotFound, "Ese usuario no existe en la base de datos");
            }

            foreach (var u in user)
            {
                if (u.contrasena != login.contrasena)
                {
                    return Content(HttpStatusCode.Conflict, "Contraseña invalida");
                }

            }
            var nuser = from i in db.admin
                        where i.usuario == login.usuario
                        select new { i.usuario, i.correo, i.id };

            return Ok(nuser);

        }


        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool adminExists(int id)
        {
            return db.admin.Count(e => e.id == id) > 0;
        }
    }
}