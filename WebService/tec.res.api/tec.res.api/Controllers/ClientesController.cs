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
using Newtonsoft.Json;
namespace tec.res.api.Controllers
{
    public class ClientesController : ApiController
    {
        // Se inicializa el modelo de datos, para poder realizar las peticiones a la Base de Datos
        private TEConstruyeEntities db = new TEConstruyeEntities();
        // URL para hacer las peticiones al API de TECres y comprobar si ya existe un usuario
        readonly string URL = "https://tecrescliente.azurewebsites.net/api/Clientes/";

        // GET: api/Clientes
        // Este método permite obtener la lista de todos los clientes, los select se realizan mediante LINQ expresions
        public object Getcliente()
        {
            var clientes = from c in db.cliente
                           select new { c.nombre, c.apellido1, c.apellido2, c.cedula, c.id, c.numero_telefono };
            return clientes;
        }
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // Obtiene un cliente en especifico
        // GET: api/Clientes/5
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Getcliente(int id)
        {
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }
        // Permite actualiza un cliente, se debe proveer el nuevo cliente y ademas el id que corresponde a la llave primaria
        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcliente(int id, cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.id)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
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
        // Con este método se puede añadir un nuevo cliente
        // Ademas se valida que no exista en TECres
        // POST: api/Clientes
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Postcliente(cliente cliente)
        {
            // Con esto podemos hacer una petición al API de TECres
            // Validamos si existe un cliente en dicha base por la cedula

            var json = new WebClient().DownloadString(URL + "Exists/" + cliente.cedula);
            if (Convert.ToBoolean(json))
            {
                return Content(HttpStatusCode.Conflict, "Ya existe ese cliente en TECres");
            }

            db.cliente.Add(cliente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (clienteExists(cliente.cedula))
                {
                    return Content(HttpStatusCode.Conflict, "Ya existe ese cliente en TEConstruye");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.id }, cliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Deletecliente(int id)
        {
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.cliente.Remove(cliente);
            await db.SaveChangesAsync();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clienteExists(string cedula)
        {
            return db.cliente.Count(e => e.cedula == cedula) > 0;
        }
        private bool clienteExists(int id)
        {
            return db.cliente.Count(e => e.id == id) > 0;
        }
    }
}