using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using teconstruye.api.Models;
using Microsoft.AspNetCore.Cors;

namespace teconstruye.api.Controllers
{
    [Route("[controller]")]
    [ApiController]

    
    public class EspecialidadController : ControllerBase
    {

        readonly NpgsqlConnection conn = new NpgsqlConnection(connectionString: "Server=dbteconstruye.postgres.database.azure.com;Database=TEConstruye;Port=5432;User Id=su@dbteconstruye;Password=T3construye;Ssl Mode=Require;");
        
        [HttpGet]
        [EnableCors("AllowMyOrigin")]
        public  IActionResult GetEspecialidad()
        {
            try
            {
                var especialidad = conn.Query<Especialidad>(sql: "SELECT * FROM especialidad");
                return Ok(new { Result = especialidad, Status = "Ok" });
            }
            catch (PostgresException e)
            {
                return Ok(new { Result = "", Status = e.Message });
            }
        }
        [Route("byid")]
        [HttpGet]
        public IActionResult GetEspecialidad(int id)
        {
            try
            {
                var especialidad = conn.Query<Especialidad>(sql: "SELECT * FROM especialidad WHERE id = "+id.ToString());
                return Ok(new { Result = especialidad, Status = "Ok" });
            }
            catch (PostgresException e)
            {
                return Ok(new { Result = "", Status = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEspecialidad (string nombre)
        {
            try
            {
                await conn.ExecuteAsync(sql: "INSERT INTO especialidad (nombre) VALUES ('" + nombre + "')");
                return Ok(new { Result = "", Status = "Ok" });

            } catch (PostgresException e)
            {
                return Ok(new { Result = "", Status = e.MessageText });
            }
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            try
            {
                await conn.ExecuteAsync(sql: "DELETE FROM Especialidad WHERE id = " + id.ToString());
                return Ok(new { Result = "", Status = conn.FullState });

            }
            catch (PostgresException e)
            {
                return Ok(new { Result = "", Status = e.MessageText });
            }
        }
    }
    
}