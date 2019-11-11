using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using tec.api.fm.Models;

namespace tec.api.fm.Controllers
{
    public class especialidadController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/especialidad
        public object Getespecialidad()
        {
            var espe = db.especialidad
                       .Select(e => new { e.nombre, e.id })
                       .ToList();
            return espe;
            
        }

        // GET: api/especialidad/5
        [ResponseType(typeof(especialidad))]
        public IHttpActionResult Getespecialidad(int id)
        {
            especialidad especialidad = db.especialidad.Find(id);
            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }

        // PUT: api/especialidad/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putespecialidad(int id, especialidad especialidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != especialidad.id)
            {
                return BadRequest();
            }

            db.Entry(especialidad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!especialidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/especialidad
        [ResponseType(typeof(especialidad))]
        public IHttpActionResult Postespecialidad(especialidad especialidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.especialidad.Add(especialidad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (especialidadExists(especialidad.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = especialidad.id }, especialidad);
        }

        // DELETE: api/especialidad/5
        [ResponseType(typeof(especialidad))]
        public IHttpActionResult Deleteespecialidad(int id)
        {
            especialidad especialidad = db.especialidad.Find(id);
            if (especialidad == null)
            {
                return NotFound();
            }

            db.especialidad.Remove(especialidad);
            db.SaveChanges();

            return Ok(especialidad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool especialidadExists(int id)
        {
            return db.especialidad.Count(e => e.id == id) > 0;
        }
    }
}