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
    public class adminController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/admin
        public IQueryable<admin> Getadmin()
        {
            return db.admin;
        }

        // GET: api/admin/5
        [ResponseType(typeof(admin))]
        public async Task<IHttpActionResult> Getadmin(int id)
        {
            admin admin = await db.admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }

        // PUT: api/admin/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putadmin(int id, admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.id)
            {
                return BadRequest();
            }

            db.Entry(admin).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!adminExists(id))
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

        // POST: api/admin
        [ResponseType(typeof(admin))]
        public async Task<IHttpActionResult> Postadmin(admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.admin.Add(admin);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (adminExists(admin.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = admin.id }, admin);
        }

        // DELETE: api/admin/5
        [ResponseType(typeof(admin))]
        public async Task<IHttpActionResult> Deleteadmin(int id)
        {
            admin admin = await db.admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            db.admin.Remove(admin);
            await db.SaveChangesAsync();

            return Ok(admin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool adminExists(int id)
        {
            return db.admin.Count(e => e.id == id) > 0;
        }
    }
}