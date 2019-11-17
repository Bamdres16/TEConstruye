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
    public class materialController : ApiController
    {
        // Se inicializa la base de datos para poder acceder
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/material
        // Metodo para obtener la lista de todos los materiales
        public object Getmaterial()
        {
            var mat = from m in db.material
                      select new { m.nombre, m.precio_unitario, m.codigo };
            return mat;
        }
        // Método para obtener un material por el código de material
        // GET: api/material/5
        [ResponseType(typeof(material))]
        public async Task<IHttpActionResult> Getmaterial(string id)
        {
            material material = await db.material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }
        // Método para actualizar un nuevo material
        // PUT: api/material/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmaterial(string id, material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != material.codigo)
            {
                return BadRequest();
            }

            db.Entry(material).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!materialExists(id))
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
        // Permite añadir un nuevo material
        // POST: api/material
        [ResponseType(typeof(material))]
        public async Task<IHttpActionResult> Postmaterial(material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.material.Add(material);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (materialExists(material.codigo))
                {
                    return Content(HttpStatusCode.Conflict, "Esa material ya está registrado");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = material.codigo }, material);
        }
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // Permite eliminar un material por su código de material
        // DELETE: api/material/5
        [ResponseType(typeof(material))]
        public async Task<IHttpActionResult> Deletematerial(string id)
        {
            material material = await db.material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            db.material.Remove(material);
            await db.SaveChangesAsync();

            return Ok(material);
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

        private bool materialExists(string id)
        {
            return db.material.Count(e => e.codigo == id) > 0;
        }
    }
}