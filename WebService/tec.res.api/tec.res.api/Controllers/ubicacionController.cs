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
    public class ubicacionController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/ubicacion obtiene las provincias

        public object GetUbicacion()
        {
            var ubicaciones = from u in db.ubicacion
                              select new { u.provincia };
            return ubicaciones.Distinct();
        }
        // GET: api/ubicacion?Provincia obtiene los cantones que corresponden a la provincia dada
        public object GetUbicacion(string Provincia)
        {
            var ubicaciones = from u in db.ubicacion
                              where u.provincia == Provincia
                              select new { u.canton };
                             
            return ubicaciones.Distinct();
        }

        // GET: api/ubicacion?Provincia=NOMBRE?Canton=NOMBRE obtiene los distritos que corresponden a la provincia y canton dados.
        public object GetUbicacion(string Provincia, string Canton)
        {
            var ubicaciones = from u in db.ubicacion
                              where (u.provincia == Provincia) & (u.canton == Canton)
                              select new { u.distrito , u.id};

            return ubicaciones.Distinct();
        }
        // Metodos autogenerados --
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ubicacionExists(int id)
        {
            return db.ubicacion.Count(e => e.id == id) > 0;
        }
    }
}