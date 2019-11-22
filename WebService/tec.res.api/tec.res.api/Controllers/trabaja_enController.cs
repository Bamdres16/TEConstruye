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
    public class trabaja_enController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/trabaja_en
        public IQueryable<trabaja_en> Gettrabaja_en()
        {
            return db.trabaja_en;
        }

        // GET: api/trabaja_en/5
        [ResponseType(typeof(trabaja_en))]
        public async Task<IHttpActionResult> Gettrabaja_en(int id)
        {
            trabaja_en trabaja_en = await db.trabaja_en.FindAsync(id);
            if (trabaja_en == null)
            {
                return NotFound();
            }

            return Ok(trabaja_en);
        }

        // PUT: api/trabaja_en/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttrabaja_en(int id, trabaja_en trabaja_en)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trabaja_en.id_arquitecto)
            {
                return BadRequest();
            }

            db.Entry(trabaja_en).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!trabaja_enExists(id))
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

        // POST: api/trabaja_en
        [ResponseType(typeof(trabaja_en))]
        public async Task<IHttpActionResult> Posttrabaja_en(List<trabaja_en> trabaja_en)
        {
            foreach (trabaja_en arquitecto in trabaja_en)
            {
                db.trabaja_en.Add(arquitecto);
                try
                {
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateException)
                {
                    if (trabaja_enExists(arquitecto.id_arquitecto))
                    {
                        return Content(HttpStatusCode.Conflict, "El arquitecto " + arquitecto.id_arquitecto + " ya tiene horas asignadas en el proyecto actual");
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Conflict();
        }

        // DELETE: api/trabaja_en/5
        [ResponseType(typeof(trabaja_en))]
        public async Task<IHttpActionResult> Deletetrabaja_en(int id)
        {
            trabaja_en trabaja_en = await db.trabaja_en.FindAsync(id);
            if (trabaja_en == null)
            {
                return NotFound();
            }

            db.trabaja_en.Remove(trabaja_en);
            await db.SaveChangesAsync();

            return Ok(trabaja_en);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool trabaja_enExists(int id)
        {
            return db.trabaja_en.Count(e => e.id_arquitecto == id) > 0;
        }
    }
}