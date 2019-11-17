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
    public class especialidadController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/especialidad
        // Permite obtener la lista de las especialidades agregadas en la base de datos
        public object Getespecialidad()
        {
            var espe = from e in db.especialidad
                       select new { e.id, e.nombre };
            return espe;
        }
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/especialidad/5
        // Obtiene el registro de la especialidad por su id
        [ResponseType(typeof(especialidad))]
        public async Task<IHttpActionResult> Getespecialidad(int id)
        {
            especialidad especialidad = await db.especialidad.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }

        // PUT: api/especialidad/5
        // Permite actualizar una especialidad añadida por su id
        // Ademas recibe en un JSON los nuevos atributos
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putespecialidad(int id, especialidad especialidad)
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
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (!especialidadExists(id))
                {
                    return NotFound();
                }
                else if ((string)e.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    return Content(HttpStatusCode.Conflict, e.InnerException.InnerException.Message);
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/especialidad
        // Añade una nueva especialidad, y valida si ya existe
        [ResponseType(typeof(especialidad))]
        public async Task<IHttpActionResult> Postespecialidad(especialidad especialidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.especialidad.Add(especialidad);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if ((string)e.InnerException.InnerException.Data["SqlState"] == "23505")
                {
                    return Content(HttpStatusCode.Conflict, "Esa especialidad ya existe");
                }


            }

            return CreatedAtRoute("DefaultApi", new { id = especialidad.id }, especialidad);
        }

        // DELETE: api/especialidad/5
        // Elimina una especialidad, por id
        [ResponseType(typeof(especialidad))]
        public async Task<IHttpActionResult> Deleteespecialidad(int id)
        {
            especialidad especialidad = await db.especialidad.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound();
            }

            db.especialidad.Remove(especialidad);
            await db.SaveChangesAsync();

            return Ok(especialidad);
        }
        // Métodos autogenerados
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