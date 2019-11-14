using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using tec.api.res.Models;

namespace tec.api.res.Controllers
{
    public class etapasController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/etapas
        // Obtiene la lista de todas las etapas que existen en la base de de datos
        // Los atributos de cada registro son Nombre, Descripcion y el Id
        public object Getetapa()
        {
            var etapas = from e in db.etapa
                         select new { e.nombre, e.descripcion, e.id };
            return etapas;
        }

        // GET: api/etapas/5
        // Obtiene el registro de una etapa en especifico por su id
        [ResponseType(typeof(etapa))]
        public async Task<IHttpActionResult> Getetapa(int id)
        {
            etapa etapa = await db.etapa.FindAsync(id);
            if (etapa == null)
            {
                return NotFound();
            }

            return Ok(etapa);
        }

        // PUT: api/etapas/5
        // Permite modificar un registro mediante su identificador
        // Se actualiza con el id, y etapa corresponde a los nuevos registros en ese id
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putetapa(int id, etapa etapa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != etapa.id)
            {
                return BadRequest();
            }

            db.Entry(etapa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!etapaExists(id))
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

        // POST: api/etapas
        // Permite añadir una nueva etapa, en este caso valida que la etapa no exista ya que el nombre es unico
        // La entrada son los valores nuevos a agregar en un JSON, la especificación de los atributos de entrada
        // está en el documento de descripción de métodos
        [ResponseType(typeof(etapa))]
        public async Task<IHttpActionResult> Postetapa(etapa etapa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.etapa.Add(etapa);
            try
            {
                await db.SaveChangesAsync();
            } catch (Exception e)
            {
                if ((string)e.InnerException.InnerException.Data["SqlState"] == "23505")
                {
                    return Content(HttpStatusCode.Conflict, "Esa etapa ya existe");
                }
                
                
            }
            return CreatedAtRoute("DefaultApi", new { id = etapa.id }, etapa);
        }

        // DELETE: api/etapas/5
        // Permite elimnar un registro mediante el id
        [ResponseType(typeof(etapa))]
        public async Task<IHttpActionResult> Deleteetapa(int id)
        {
            etapa etapa = await db.etapa.FindAsync(id);
            if (etapa == null)
            {
                return NotFound();
            }
           
            db.etapa.Remove(etapa);
            try
            {
                await db.SaveChangesAsync();
            } catch (Exception e)
            {
                
                return Content(HttpStatusCode.Conflict, e.InnerException.InnerException.Message);
            }
            return Ok(etapa);
        }
        // Métodos autogenerados ---
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool etapaExists(int id)
        {
            return db.etapa.Count(e => e.id == id) > 0;
        }
    }
}