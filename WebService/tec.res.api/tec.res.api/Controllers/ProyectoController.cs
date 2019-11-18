using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using tec.res.api.Models;

namespace tec.res.api.Controllers
{
    public class ProyectoController : ApiController
    {
        // Se añade la base de datos
        private TEConstruyeEntities db = new TEConstruyeEntities();



        // Este método permite asignar las etapas a un proyecto
        [Route("api/Proyecto/asignaretapa")]
        [HttpPut]
        public async Task<IHttpActionResult> putAsignarEtapa (tiene etapas)
        {
            
            db.tiene.Add(etapas);
            try
            {
                await db.SaveChangesAsync();

            } catch (Exception)
            {
                if (etapaExist(etapas.id_obra, etapas.id_etapa)) { 
                    return Content(HttpStatusCode.Conflict, "Esa etapa ya está asociada a la obra");
                } else
                {
                    throw;
                }
            }
            return Ok();
            
        }
        // Este método permite asignar las etapas a un proyecto
        [Route("api/Proyecto/etapas/{obra}")]
        [HttpGet]
        public  IHttpActionResult getEtapas(int obra)
        {

            var etapas = from e in db.tiene
                         where e.id_obra == obra
                         select new { e.id_obra, e.id_etapa, e.fecha_incio, e.fecha_finalizacion };
            return Ok(etapas);

        }

        // Método para determinar si existe una etapa asociada a una obra
        private bool etapaExist(int id_obra, int id_etapa)
        {
            return db.tiene.Count(e => ((e.id_obra == id_obra) && (e.id_etapa == id_etapa))) > 0;
        }
        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        [Route("api/Proyecto/asignaretapa")]
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        

    }
}
