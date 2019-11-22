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
    public class ReporteController : ApiController
    {
       
        TEConstruyeEntities db = new TEConstruyeEntities();
        readonly string ConnectionString = "Server=dbteconstruyeb.postgres.database.azure.com;Database=TEConstruye;Port=5432;User Id=su@dbteconstruyeb;Password=T3construye;Ssl Mode=Require;";
        [Route("api/Reporte/Planilla")]
        [HttpGet]
        public object getPlanilla ()
        {
            var conn = new NpgsqlConnection(ConnectionString);
            var sql = "Select * from Planilla()";
            List<object> reporte1 = new List<object>();
           
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reporte1.Add(new {nombre_obra = reader[0], id = reader[1], nombre_empleado = reader[2], semana = reader[3], pago_semana = reader[4] });
            }
            conn.Close();
            
            return reporte1;
            
        }

        // Este método permite retonar una respuesta a las peticiones, evitando cualquier problema de CORS
        [Route("api/Reporte/Planilla")]
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
    }
}
