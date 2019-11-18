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
    public class empleadosController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();
        // Obtener la lista de todos los empleados
        // GET: api/empleados
        public object Getempleado()
        {
            return from e in db.empleado
                   select new { e.nombre, e.apellido1, e.apellido2, e.cedula, e.numero_telefono, e.pago_hora, e.id};
        }
        // Para obtener los datos de un empleado especifico
        // GET: api/empleados/5
        [ResponseType(typeof(empleado))]
        public async Task<IHttpActionResult> Getempleado(int id)
        {
            empleado empleado = await db.empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // Para actualizar los datos de empleado
        // PUT: api/empleados/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putempleado(int id, empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empleado.id)
            {
                return BadRequest();
            }

            db.Entry(empleado).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!empleadoExists(id))
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
        // Para almacenar un nuevo empleado
        // POST: api/empleados
        [ResponseType(typeof(empleado))]
        public async Task<IHttpActionResult> Postempleado(empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.empleado.Add(empleado);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (cedulaExists(empleado.cedula))
                {
                    return Content(HttpStatusCode.Conflict, "Ese empleado ya existe");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = empleado.id }, empleado);
        }
        // Para eliminar un empleado en especifico
        // DELETE: api/empleados/5
        [ResponseType(typeof(empleado))]
        public async Task<IHttpActionResult> Deleteempleado(int id)
        {
            empleado empleado = await db.empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            db.empleado.Remove(empleado);
            await db.SaveChangesAsync();

            return Ok(empleado);
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

        private bool empleadoExists(int id)
        {
            return db.empleado.Count(e => e.id == id) > 0;
        }
        private bool cedulaExists (string cedula)
        {
            return db.empleado.Count(e => e.cedula == cedula) > 0;
        }
    }
}