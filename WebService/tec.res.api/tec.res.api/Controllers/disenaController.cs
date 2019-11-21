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
using tec.res.api.Models;

namespace tec.res.api.Controllers
{
    public class disenaController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/disena
        public IQueryable<diseña> Getdiseña()
        {
            return db.diseña;
        }

        // GET: api/disena/5
        [ResponseType(typeof(diseña))]
        public async Task<IHttpActionResult> Getdiseña(int id)
        {
            diseña diseña = await db.diseña.FindAsync(id);
            if (diseña == null)
            {
                return NotFound();
            }

            return Ok(diseña);
        }

        // PUT: api/disena/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdiseña(int id, diseña diseña)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diseña.id_ingeniero)
            {
                return BadRequest();
            }

            db.Entry(diseña).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!diseñaExists(id))
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

        
        // POST: api/diseña
        [ResponseType(typeof(diseña))]
        public async Task<IHttpActionResult> Postdiseña(List<diseña> diseña)
        {
           
            foreach (diseña ingenieros in diseña)
            {
                db.diseña.Add(ingenieros);
                try
                {
                    await db.SaveChangesAsync();
                    return Ok ();
                }
                catch (DbUpdateException)
                {
                    if (diseñaExists(ingenieros.id_ingeniero))
                    {
                        return Content(HttpStatusCode.Conflict, "El ingeniero " + ingenieros.id_ingeniero + " ya tiene horas asignadas en esa semana para el proyecto actual");
                    }
                    else
                    {
                        throw;
                    }
                }
                
                

            }
            return Conflict();
        }

        // DELETE: api/disena/5
        [ResponseType(typeof(diseña))]
        public async Task<IHttpActionResult> Deletediseña(int id)
        {
            diseña diseña = await db.diseña.FindAsync(id);
            if (diseña == null)
            {
                return NotFound();
            }

            db.diseña.Remove(diseña);
            await db.SaveChangesAsync();

            return Ok(diseña);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool diseñaExists(int id)
        {
            return db.diseña.Count(e => e.id_ingeniero == id) > 0;
        }
    }
}