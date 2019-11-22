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

        [Route("api/Reporte/Gastos")]
        [HttpGet]
        public object getGastos()
        {
            var conn = new NpgsqlConnection(ConnectionString);
            var sql = "Select * from Gastos()";
            List<object> reporte1 = new List<object>();

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reporte1.Add(new { semana = reader[0], nombre_obra = reader[1], monto_gastado = reader[2], nombre_etapa = reader[3]});
            }
            conn.Close();

            return reporte1;

        }
        public struct State
        {
            public int semana { get; set; }
            public string nombre_obra { get; set; }
            public string monto_gastado { get; set; }
            public string presupuesto { get; set; }
            public string nombre_etapa { get; set; }
        }
        [Route("api/Reporte/Estado")]
        [HttpGet]
        public object getEstado()
        {
            var conn = new NpgsqlConnection(ConnectionString);
            var sql = "Select * from Estado()";
            List<State> reporte1 = new List<State>();

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reporte1.Add(new State
                {
                    semana = (int)reader[0],
                    nombre_obra = (string)reader[1],
                    monto_gastado = (string)reader[2],
                    presupuesto = (string)reader[3],
                    nombre_etapa = (string)reader[4]
                });
            }
            List<object> porSemana = new List<object>();
            List<object> total = new List<object>();
            conn.Close();
            int semana_actual = 1;
            double total_semana_presupuesto = 0;
            double total_semana_monto = 0;
             foreach (State state in reporte1)
            {
                if (state.semana.Equals(semana_actual))
                {
                    total_semana_monto += Convert.ToDouble(state.monto_gastado);
                    total_semana_presupuesto += Convert.ToDouble(state.presupuesto);
                    porSemana.Add(new { state.nombre_obra, state.nombre_etapa, state.monto_gastado, state.presupuesto });

                } else
                {
                    total.Add(new { semana = semana_actual, porSemana, total_semana_monto, total_semana_presupuesto});
                    porSemana = new List<object>();
                    semana_actual += 1;
                    total_semana_monto = 0;
                    total_semana_presupuesto = 0;

                }
            }

            return total;

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
