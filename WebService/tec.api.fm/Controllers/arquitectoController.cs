using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using tec.api.fm.Models;

namespace tec.api.fm.Controllers
{
    public class arquitectoController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/arquitecto
        public IQueryable<arquitecto> Getarquitecto()
        {
            return db.arquitecto;
        }

        // GET: api/arquitecto/5
        [ResponseType(typeof(arquitecto))]
        public IHttpActionResult Getarquitecto(int id)
        {
            arquitecto arquitecto = db.arquitecto.Find(id);
            if (arquitecto == null)
            {
                return NotFound();
            }

            return Ok(arquitecto);
        }

        // PUT: api/arquitecto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putarquitecto(int id, arquitecto arquitecto)
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
                db.SaveChanges();
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

        // POST: api/arquitecto
        [ResponseType(typeof(arquitecto))]
        public IHttpActionResult Postarquitecto(arquitecto arquitecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.arquitecto.Add(arquitecto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (arquitectoExists(arquitecto.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = arquitecto.id }, arquitecto);
        }

        // DELETE: api/arquitecto/5
        [ResponseType(typeof(arquitecto))]
        public IHttpActionResult Deletearquitecto(int id)
        {
            arquitecto arquitecto = db.arquitecto.Find(id);
            if (arquitecto == null)
            {
                return NotFound();
            }

            db.arquitecto.Remove(arquitecto);
            db.SaveChanges();

            return Ok(arquitecto);
        }

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
    }
}