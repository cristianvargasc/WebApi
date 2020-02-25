using System.Collections.Generic;
using System.Linq;
using webapi_csharp.Modelos;
using webapi_csharp.Controllers;
using System.Data;
using System;

namespace webapi_csharp.Repositorios
{
    public class RPFuncionarios
    {
        List<Funcionario> lstClientes = new List<Funcionario>();
        List<Registro> lstRegistro = new List<Registro>();

        public static List<Funcionario> ObtenerListaClientes(string documento = "")
        {
            ConexionSql ConSql = new ConexionSql();
            List<Funcionario> lstCliente = new List<Funcionario>();

            //MOSTRAR
            lstCliente = ConSql.Mostrar(documento.ToString()) ;

            foreach (var item in lstCliente)
            {
                new Funcionario() {
                    fun_id = item.fun_id,
                    fun_nombre = item.fun_nombre,
                    fun_segundo_nombre = item.fun_segundo_nombre,
                    fun_apellidos = item.fun_apellidos,
                    fun_correo = item.fun_correo
                };
            }

            return lstCliente;
        }

        public static List<Registro> ObtenerRegistroFuncionario(string documento)
        {
            ConexionSql ConSql = new ConexionSql();
            List<Registro> lstCliente = new List<Registro>();

            //MOSTRAR
            lstCliente = ConSql.MostrarRegistro(documento.ToString());

            foreach (var item in lstCliente)
            {
                new Registro()
                {
                    documento = item.documento,
                    type = item.type,
                    time = item.time
                };
            }
            return lstCliente;
        }

        public IEnumerable<Funcionario> ObtenerClientes()
        {
            lstClientes = ObtenerListaClientes();
            return lstClientes;
        }

        public Funcionario ObtenerCliente(int documento)
        {
            lstClientes = ObtenerListaClientes(documento.ToString());
            var cliente = lstClientes.Where(cli => cli.fun_documento == documento);

            return cliente.FirstOrDefault();
        }

        public IEnumerable<Registro> ObtenerRegistro(int documento)
        {
            lstRegistro = ObtenerRegistroFuncionario(documento.ToString());
            return lstRegistro;
        }

        public IEnumerable<Autorizado> ObtenerIngreso(int id)
        {
            List<Autorizado> lstAutorizado = new List<Autorizado>();

            lstAutorizado.Add(new Autorizado { documento = id.ToString(), type = "out", time = DateTime.Now.ToString() });

            return lstAutorizado; 
        }

        public void Agregar(Funcionario nuevoCliente)
        {
            lstClientes.Add(nuevoCliente);
        }
    }
}
