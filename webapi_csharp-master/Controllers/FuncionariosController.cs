using Microsoft.AspNetCore.Mvc;
using webapi_csharp.Modelos;
using webapi_csharp.Repositorios;

namespace webapi_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            RPFuncionarios rpCli = new RPFuncionarios();
            return Ok(rpCli.ObtenerClientes());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RPFuncionarios rpCli = new RPFuncionarios();

            var cliRet = rpCli.ObtenerCliente(id);

            if (cliRet == null)
            {
                var nf = NotFound("El cliente " + id.ToString() + " no existe.");
                return nf;
            }

            return Ok(cliRet);
        }

        [HttpPost("agregar")]
        public IActionResult AgregarCliente(Funcionario nuevoCliente)
        {
            RPFuncionarios rpCli = new RPFuncionarios();
            rpCli.Agregar(nuevoCliente);
            return CreatedAtAction(nameof(AgregarCliente), nuevoCliente);
        }
    }
}