using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using tec.res.api.Models;

namespace tec.res.api.Controllers
{
    public class gastosController : ApiController
    {
        private TEConstruyeEntities db = new TEConstruyeEntities();
        private string path = @"D:\Escritorio\TEConstruye\TEConstruye\Facturas\";
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
        public struct Gastos
        {
            public string proveedor { get; set; }
            public string foto { get; set; }
            public string numero_factura { get; set; }
            public int id_etapa { get; set; }
            public int semana { get; set; }
            public int id_obra { get; set; }
            public string monto { get; set; }
            public string presupuesto { get; set; }
            public List<string> material { get; set; }
        }
        // POST: api/gastos
        
        [ResponseType(typeof(gasto))]
        public async Task<IHttpActionResult> Postgasto(Gastos gasto)
        {
            List<material> materiales = new List<material>();
            foreach (string codigo in gasto.material)
            {
                material material = db.material.Find(codigo);
                materiales.Add(material);
            }
            
            gasto gasto1 = new gasto();
            gasto1 = new gasto
            {

                id_etapa = gasto.id_etapa,
                id_obra = gasto.id_obra,
                numero_factura = gasto.numero_factura,
                proveedor = gasto.proveedor,
                foto = "",
                semana = gasto.semana,
                monto  = gasto.monto,
                presupuesto =gasto.presupuesto
            };
            gasto1.material = materiales;

            db.gasto.Add(gasto1);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.Conflict, e);
            }
            gasto1.foto = gasto1.id_compra + ".jpg";
            db.Entry(gasto1).State = EntityState.Modified;
            await db.SaveChangesAsync();

            var bytes = Convert.FromBase64String(gasto.foto);
            string rute = path + gasto1.id_compra + ".jpg";
            using (var imageFile = new FileStream(rute, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }


            return Ok();
        }
        //public async Task<IHttpActionResult> PostGastos(List<gasto> gastos)
        //{
        //    foreach (gasto gastoi in gastos)
        //    {
        //        db.gasto.Add(gastoi);
        //        await db.SaveChangesAsync();
        //    }
        //    return Ok();
            
        //}
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