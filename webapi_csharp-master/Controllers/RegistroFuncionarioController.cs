using Microsoft.AspNetCore.Mvc;
using webapi_csharp.Modelos;
using webapi_csharp.Repositorios;

namespace webapi_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroFuncionarioController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RPFuncionarios rpCli = new RPFuncionarios();

            var cliRet = rpCli.ObtenerRegistro(id);

            if (cliRet == null)
            {
                var nf = NotFound("El funcionario con documento: " + id.ToString() + " no contiene ingresos o salidas.");
                return nf;
            }

            return Ok(cliRet);
        }
    }
}