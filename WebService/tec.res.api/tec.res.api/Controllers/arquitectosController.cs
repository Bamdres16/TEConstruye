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
    public class arquitectosController : ApiController
    {   // Se inicializa el modelo de datos, para poder realizar las peticiones a la Base de Datos

        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/arquitectos
        // Este método permite obtener la lista de todos los arquitectos, los select se realizan mediante LINQ expresions
        public object Getarquitecto()
        {
            var arquitectos = from i in db.arquitecto
                             select new { i.nombre, i.apellido1, i.apellido2, i.cedula, i.codigo_arquitecto, i.numero_telefono, i.id_especialidad, i.id };
            return arquitectos;
        }

        // GET: api/arquitectos/5
        // Obtiene un arquitecto en especifico
        [ResponseType(typeof(arquitecto))]
        public async Task<IHttpActionResult> Getarquitecto(int id)
        {
            arquitecto arquitecto = await db.arquitecto.FindAsync(id);
            if (arquitecto == null)
            {
                return NotFound();
            }

            return Ok(arquitecto);
        }
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }

        // PUT: api/arquitectos/5
        // Permite actualizar un arquitecto, se debe proveer el nuevo arquitecto y ademas el id que corresponde a la llave primaria
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putarquitecto(int id, arquitecto arquitecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arquitecto.id)
            {
                return BadRequest();
            }

            db.Entry(arquitecto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!arquitectoExists(id))
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
        // Con este método se puede añadir un nuevo arquitecto
        // POST: api/arquitectos
        [ResponseType(typeof(arquitecto))]
        public async Task<IHttpActionResult> Postarquitecto(arquitecto arquitecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.arquitecto.Add(arquitecto);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (arquitectoExists(arquitecto.cedula))
                {
                    return Content(HttpStatusCode.Conflict, "Esa cédula ya está en TEConstruye");
                }
                else if (codigoExists(arquitecto.codigo_arquitecto))
                {
                    return Content(HttpStatusCode.Conflict, "Ese código de arquitecto ya está en TEConstruye");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = arquitecto.id }, arquitecto);
        }
        // Permite eliminar un arquitecto en especifico por su id
        // DELETE: api/arquitectos/5
        [ResponseType(typeof(arquitecto))]
        public async Task<IHttpActionResult> Deletearquitecto(int id)
        {
            arquitecto arquitecto = await db.arquitecto.FindAsync(id);
            if (arquitecto == null)
            {
                return NotFound();
            }

            db.arquitecto.Remove(arquitecto);
            await db.SaveChangesAsync();

            return Ok(arquitecto);
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

        private bool arquitectoExists(int id)
        {
            return db.arquitecto.Count(e => e.id == id) > 0;
        }
        private bool arquitectoExists(string id)
        {
            return db.arquitecto.Count(e => e.cedula == id) > 0;
        }
        private bool codigoExists(string id)
        {
            return db.arquitecto.Count(e => e.codigo_arquitecto == id) > 0;
        }
    }
}