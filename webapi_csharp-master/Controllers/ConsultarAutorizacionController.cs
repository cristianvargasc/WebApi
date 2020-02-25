﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webapi_csharp.Modelos;
using webapi_csharp.Repositorios;

namespace webapi_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultarAutorizacionController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RPFuncionarios rpCli = new RPFuncionarios();
            ConexionSql ConSql = new ConexionSql();

            //CONSULTA PARA INGRESAR
            var autorizacionIngreso = ConSql.validarFuncionario(id.ToString(), "autorizados");

            if (autorizacionIngreso)
            {
                var nf = NotFound("El funcionario con número de documento: " + id.ToString() + " tiene autorización de ingreso.");
                return nf;
            }
            else
            {
                var nf = NotFound("El funcionario con número de documento: " + id.ToString() + " no tiene autorización de ingreso.");
                return nf;
            }
        }
    }
}