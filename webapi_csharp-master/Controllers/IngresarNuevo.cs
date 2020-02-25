using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webapi_csharp.Modelos;
using webapi_csharp.Repositorios;

namespace webapi_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresarNuevoController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RPFuncionarios rpCli = new RPFuncionarios();
            ConexionSql ConSql = new ConexionSql();

            //CONSULTAR EXISTENCIA
            var existeFuncionario = ConSql.validarFuncionario(id.ToString(), "funcionario");

            if (existeFuncionario)
            {
                //INSERTAR EN TABLA DE AUTORIZACIONES
                var resultadoInsertar = ConSql.Insertar(id.ToString());

                if (resultadoInsertar)
                {
                    var nf = NotFound("El funcionario con número de documento: " + id.ToString() + " se le ha otorgado autorización para ingreso.");
                    return nf;
                }
                else
                {
                    var nf = NotFound("El funcionario con número de documento: " + id.ToString() + " no se le ha posido otorgar autorización para ingreso.");
                    return nf;
                }
            }
            else
            {
                var nf = NotFound("El funcionario con número de documento: " + id.ToString() + " no se encuentra en los registros.");
                return nf;
            }
        }
    }
}