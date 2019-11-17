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
    public class ingenierosController : ApiController
    {   // Se inicializa el modelo de datos, para poder realizar las peticiones a la Base de Datos

        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/ingenieros
        // Este método permite obtener la lista de todos los ingenieros, los select se realizan mediante LINQ expresions
        public object Getingeniero()
        {
            var ingenieros = from i in db.ingeniero
                             select new { i.nombre, i.apellido1, i.apellido2, i.cedula, i.codigo_ingeniero, i.numero_telefono, i.id_especialidad, i.id };
            return ingenieros;
        }

        // GET: api/ingenieros/5
        // Obtiene un ingeniero en especifico
        [ResponseType(typeof(ingeniero))]
        public async Task<IHttpActionResult> Getingeniero(int id)
        {
            ingeniero ingeniero = await db.ingeniero.FindAsync(id);
            if (ingeniero == null)
            {
                return NotFound();
            }

            return Ok(ingeniero);
        }
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }

        // PUT: api/ingenieros/5
        // Permite actualiza un ingeniero, se debe proveer el nuevo ingeniero y ademas el id que corresponde a la llave primaria
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putingeniero(int id, ingeniero ingeniero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingeniero.id)
            {
                return BadRequest();
            }

            db.Entry(ingeniero).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ingenieroExists(id))
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
        // Con este método se puede añadir un nuevo ingeniero
        // POST: api/ingenieros
        [ResponseType(typeof(ingeniero))]
        public async Task<IHttpActionResult> Postingeniero(ingeniero ingeniero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ingeniero.Add(ingeniero);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ingenieroExists(ingeniero.cedula))
                {
                    return Content(HttpStatusCode.Conflict, "Esa cédula ya está en TEConstruye");
                } else if (codigoExists(ingeniero.codigo_ingeniero))
                {
                    return Content(HttpStatusCode.Conflict, "Esa código de ingeniero ya está en TEConstruye");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ingeniero.id }, ingeniero);
        }
        // Permite eliminar un ingeniero en especifico por su id
        // DELETE: api/ingenieros/5
        [ResponseType(typeof(ingeniero))]
        public async Task<IHttpActionResult> Deleteingeniero(int id)
        {
            ingeniero ingeniero = await db.ingeniero.FindAsync(id);
            if (ingeniero == null)
            {
                return NotFound();
            }

            db.ingeniero.Remove(ingeniero);
            await db.SaveChangesAsync();

            return Ok(ingeniero);
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

        private bool ingenieroExists(int id)
        {
            return db.ingeniero.Count(e => e.id == id) > 0;
        }
        private bool ingenieroExists(string id)
        {
            return db.ingeniero.Count(e => e.cedula == id) > 0;
        }
        private bool codigoExists(string id)
        {
            return db.ingeniero.Count(e => e.codigo_ingeniero == id) > 0;
        }
    }
}