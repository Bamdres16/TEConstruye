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
        readonly string ConnectionString = "Server=dbteconstruyeb.postgres.database.azure.com;Database=TEConstruye;Port=5432;User Id=su@dbteconstruyeb;Password=T3construye;Ssl Mode=Require;";


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
        [Route("api/Proyecto/presupuesto")]
        [HttpGet]
        public object GetPresupuesto ()
        {
            var conn = new NpgsqlConnection(ConnectionString);
            var sql = "Select * from Presupuesto1()";
            List<object> reporte1 = new List<object>();

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reporte1.Add(new { nombre_etapa = reader[0], nombre_obra = reader[1], precio_etapa = reader[2]});
            }
            conn.Close();

            return reporte1;
        }
        // Struct para publicar un propiedad en TECres
        struct Propiedad
        {
            public double tamano_construccion { get; set; }
            public double tamano_terreno { get; set; }
            public int niveles { get; set; }
            public string tipo_piso { get; set; }
            public bool piscina { get; set; }
            public bool gimnasio { get; set; }
            public int cantidad_banos { get; set; }
            public int cant_habitaciones { get; set; }
            public int espacios_parqueo { get; set; }
            public bool parqueo_visitas { get; set; }
            public string titulo { get; set; }
            public int ubicacion { get; set; }
            public int id_dueno { get; set; }
            public string inmueble { get; set; }

        }



        //// Este método permite publicar una obra de TEConstruye en TECres
        //// URL de TEConstruye
        //readonly string URL = "https://tecrescliente.azurewebsites.net/api/Propiedad";
        //[Route("api/Proyecto/publicar")]
        //[HttpPost]
        //public async Task<IHttpActionResult> PostProyecto(obra obra)
        //{
        //    Propiedad propiedad = new Propiedad();
        //    propiedad.tamano_construccion = (double)obra.area_construccion;
        //    propiedad.tamano_terreno = obra.
            
        //}



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
        public IHttpActionResult OptionsHoras()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        [Route("api/Proyecto/asignarmaterial")]
        public IHttpActionResult OptionsMaterial()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }

    }
}
