﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using webapi_csharp.Modelos;
using System.Data;

namespace webapi_csharp.Controllers
{
    class ConexionSql
    {        
        private const string connectionString = "" +
                "datasource=85.10.205.173;" +
                "port=3306;" +
                "username=adminbodega;" +
                "password=ABodega2020;" +
                "database=bodega2020;" +
                "old guids=true;";

        public Boolean RegistrarInOut(string tipo, string documento)
        {
            string queryRegistro = "INSERT INTO `registro` (`reg_id`, `reg_tipo`, `reg_documento`, `fecha`) VALUES(NULL, '" + tipo + "', '" + documento + "', SYSDATE())";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(queryRegistro, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                commandDatabase.ExecuteReader();

                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }

        }

        public Boolean validarFuncionario(string documento, string tabla)
        {
            string query = string.Empty;
            if (tabla == "funcionario")
            {
                query = "SELECT * FROM `funcionario` WHERE fun_documento = '" + documento + "'";
            }
            else
            {
                query = "SELECT * FROM `autorizados` WHERE aut_documento = '" + documento + "'";
            }

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public Boolean AutorizarIngreso(float documento)
        {
            string query = "SELECT * FROM `autorizados` WHERE aut_documento = '" + documento + "'";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public Boolean Insertar(string documento)
        {
            string query = "INSERT INTO autorizados (aut_id, aut_documento) VALUES (null, '" + documento + "')";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                databaseConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public List<Funcionario> Mostrar(string documento)
        {
            List<Funcionario> listaMostrar = new List<Funcionario>();
            DataTable datos = new DataTable();

            string query = string.Empty;

            if (documento == "")
            {
                query = "SELECT * FROM funcionario";
            }
            else
            {
                query = "SELECT * FROM funcionario WHERE fun_documento = '" + documento + "'";
            }

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();

                reader = commandDatabase.ExecuteReader();

                datos.Load(reader);

                foreach (DataRow item in datos.Rows)
                {
                    listaMostrar.Add(CargarConsultaFuncionarios(item));
                }

                databaseConnection.Close();
                return listaMostrar;
            }
            catch (Exception)
            {
                return listaMostrar;
            }
        }
        private Funcionario CargarConsultaFuncionarios(DataRow registro)
        {
            Funcionario Funcionarios = new Funcionario();

            Funcionarios.fun_id = Convert.ToInt32(registro["fun_id"].ToString());
            Funcionarios.fun_nombre = registro["fun_nombre"].ToString();
            Funcionarios.fun_segundo_nombre = registro["fun_segundo_nombre"].ToString();
            Funcionarios.fun_apellidos = registro["fun_apellidos"].ToString();
            Funcionarios.fun_documento = float.Parse(registro["fun_documento"].ToString());
            Funcionarios.fun_contrasena = registro["fun_contrasena"].ToString();
            Funcionarios.fun_img_perfil = registro["fun_img_perfil"].ToString();
            Funcionarios.fun_contacto1 = registro["fun_contacto1"].ToString();
            Funcionarios.fun_contacto2 = registro["fun_contacto2"].ToString();
            Funcionarios.fun_contacto3 = registro["fun_contacto3"].ToString();
            Funcionarios.fun_correo = registro["fun_correo"].ToString();

            return Funcionarios;
        }


        public List<Registro> MostrarRegistro(string documento)
        {
            List<Registro> listaMostrarRegistro = new List<Registro>();
            DataTable datos = new DataTable();

            var query = "SELECT * FROM `registro` WHERE reg_documento = '" + documento + "'";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();

                reader = commandDatabase.ExecuteReader();

                datos.Load(reader);

                foreach (DataRow item in datos.Rows)
                {
                    listaMostrarRegistro.Add(CargarRegistroFuncionarios(item));
                }

                databaseConnection.Close();
                return listaMostrarRegistro;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return listaMostrarRegistro;
            }
        }
        private Registro CargarRegistroFuncionarios(DataRow registro)
        {
            Registro registros = new Registro();

            registros.documento = registro["reg_documento"].ToString();
            registros.type = registro["reg_tipo"].ToString();
            registros.time = registro["fecha"].ToString();

            return registros;
        }
    }
}
