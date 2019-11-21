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
    public class gastosController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();

        // GET: api/gastos
        public object Getgasto()
        {
            return from g in db.gasto
                   select new { g.id_compra, g.id_etapa, g.id_obra, g.numero_factura, g.semana };
        }

        // GET: api/gastos/5
        [ResponseType(typeof(gasto))]
        public async Task<IHttpActionResult> Getgasto(int id)
        {
            gasto gasto = await db.gasto.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(gasto);
        }

        // PUT: api/gastos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgasto(int id, gasto gasto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gasto.id_compra)
            {
                return BadRequest();
            }

            db.Entry(gasto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gastoExists(id))
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

        //// POST: api/gastos
       
        //[ResponseType(typeof(gasto))]
        //public async Task<IHttpActionResult> Postgasto(gasto gasto)
        //{
        //    if (!ModelState.IsValid)
        //    {
                


        //        return BadRequest(ModelState);
        //    }

        //    db.gasto.Add(gasto);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = gasto.id_compra }, gasto);
        //}
        public async Task<IHttpActionResult> PostGastos(List<gasto> gastos)
        {
            foreach (gasto gastoi in gastos)
            {
                db.gasto.Add(gastoi);
                await db.SaveChangesAsync();
            }
            return Ok();
            
        }
        // DELETE: api/gastos/5
        [ResponseType(typeof(gasto))]
        public async Task<IHttpActionResult> Deletegasto(int id)
        {
            gasto gasto = await db.gasto.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }

            db.gasto.Remove(gasto);
            await db.SaveChangesAsync();

            return Ok(gasto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool gastoExists(int id)
        {
            return db.gasto.Count(e => e.id_compra == id) > 0;
        }
    }
}