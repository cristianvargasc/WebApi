using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webapi_csharp.Modelos;
using webapi_csharp.Repositorios;

namespace webapi_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RPFuncionarios rpCli = new RPFuncionarios();
            ConexionSql ConSql = new ConexionSql();

            var cliRet = rpCli.ObtenerIngreso(id);
            var registroInOut = ConSql.RegistrarInOut("out", id.ToString());
            return Ok(cliRet);
        }
    }
}