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

namespace teconstruye.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {

        readonly NpgsqlConnection conn = new NpgsqlConnection(connectionString: "Server=dbteconstruye.postgres.database.azure.com;Database=TEConstruye;Port=5432;User Id=su@dbteconstruye;Password=T3construye;Ssl Mode=Require;");
        [HttpGet]
        public IActionResult GetEspecialidad()
        {

            var especialidad = conn.Query<Especialidad>(sql: "SELECT * FROM especialidad");
            
            return Ok(especialidad);
        }
        [HttpPost]
        public async Task<IActionResult> PostEspecialidad (string nombre)
        {
            try
            {
                await conn.ExecuteAsync(sql: "INSERT INTO especialidad (nombre) VALUES ('" + nombre + "')");
                return Ok();

            } catch
            {   
                return Ok("Ya existe");
            }
            
        }
    }
}