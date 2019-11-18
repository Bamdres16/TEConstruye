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
    public class obrasController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();
        // Obtiene la lista de todas las obras existentes
        // GET: api/obras
        public object Getobra()
        {
            return from o in db.obra
                   select new
                   {
                       o.area_construccion,
                       o.area_lote,
                       o.cantidad_banos,
                       o.cantidad_habitaciones,
                       o.cantidad_pisos,
                       o.id,
                       o.nombre_obra,
                       o.propietario,
                       o.ubicacion
                   };
        }
        // Obtiene una obra en específico
        // GET: api/obras/5
        [ResponseType(typeof(obra))]
        public async Task<IHttpActionResult> Getobra(int id)
        {
            obra obra = await db.obra.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            return Ok(obra);
        }
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // Actualiza los datos de una obra
        // PUT: api/obras/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putobra(int id, obra obra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obra.id)
            {
                return BadRequest();
            }

            db.Entry(obra).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!obraExists(id))
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
        // Almacena los datos de una obra
        // POST: api/obras
        [ResponseType(typeof(obra))]
        public async Task<IHttpActionResult> Postobra(obra obra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.obra.Add(obra);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = obra.id }, obra);
        }
        // Eliminar una obra por su identificador
        // DELETE: api/obras/5
        [ResponseType(typeof(obra))]
        public async Task<IHttpActionResult> Deleteobra(int id)
        {
            obra obra = await db.obra.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            db.obra.Remove(obra);
            await db.SaveChangesAsync();

            return Ok(obra);
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

        private bool obraExists(int id)
        {
            return db.obra.Count(e => e.id == id) > 0;
        }
    }
}