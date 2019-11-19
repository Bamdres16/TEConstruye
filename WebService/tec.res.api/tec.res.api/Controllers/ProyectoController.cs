using Npgsql;
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
        public async Task<IHttpActionResult> PutAsignarEtapa(tiene etapas)
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

        // Este método permite asignar las horas de un empleado
        [Route("api/Proyecto/asignarhoras")]
        [HttpPut]
        public async Task<IHttpActionResult> PutAsignarHoras(labora_en labora)
        {

            db.labora_en.Add(labora);
            try
            {
                await db.SaveChangesAsync();

            }
            catch (Exception)
            {
                if (semanaEmpleado(labora.id_obra, labora.id_empleado, (int)labora.semana))
                {
                    return Content(HttpStatusCode.Conflict, "El empleado ya tiene horas asignadas en esa semana para el proyecto actual");
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }

        // Este método permite asignar los materiales en cada etapa
        [Route("api/Proyecto/asignarmaterial")]
        [HttpPut]
        public async Task<IHttpActionResult> PutAsignarMaterial(requiere requiere)
        {

            db.requiere.Add(requiere);
            try
            {
                await db.SaveChangesAsync();

            }
            catch (Exception)
            {
                // Valida si ya existe una material en una etapa
                if (db.requiere.Count(e => ((e.id_etapa == requiere.id_etapa) && (e.codigo_material == requiere.codigo_material) && (e.id_obra == requiere.id_obra))) > 0)
                {
                    return Content(HttpStatusCode.Conflict, "El material ya está asociado con esta etapa");
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }
        
        // Este método permite generar el presupuesto de un proyecto
        [Route("api/Proyecto/presupuesto/{id}")]
        [HttpGet]
        public object GetPresupuesto (int id)
        {
            var pres = from R in db.requiere
                       join O in db.obra on R.id_obra equals O.id
                       join M in db.material on R.codigo_material equals M.codigo
                       join E in db.etapa on R.id_etapa equals E.id
                       where O.id == id
                       group new { E.nombre, Precio_Etapa = M.precio_unitario * R.cantidad } by new { E.nombre } into G
                       select new { Nombre = G.Select(e => e.nombre).FirstOrDefault(), Precio_Etapa = G.Sum(e => e.Precio_Etapa) };

            double total = 0;
            foreach (var p in pres)
            {
                total += (double) p.Precio_Etapa;
            }
                        
            return new { Etapas = pres, Total = total };

           
            
            

        }

        // Este método permite obtener las etapas de un proyecto
        [Route("api/Proyecto/etapas/{obra}")]
        [HttpGet]
        public  IHttpActionResult GetEtapas(int obra)
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
        
        private bool semanaEmpleado(int id_obra, int id_empleado, int semana)
        {
            return db.labora_en.Count(e => ((e.id_obra == id_obra) && (e.id_empleado == id_empleado) && (e.semana == semana))) > 0;
        }

        // Metodos options para cada ruta nueva en proyecto
        // Estos métodos permiten retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        [Route("api/Proyecto/asignaretapa")]
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        [Route("api/Proyecto/asignarhoras")]
        public IHttpActionResult Options_1()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        [Route("api/Proyecto/asignarmaterial")]
        public IHttpActionResult Options_2()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }

    }
}
