using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMVC.Models
{
    public class Usuarios
    {
        private IConfiguration configuration;

        private SqlDataReader dataRead = null;
        public Usuarios(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool agregarUsuario(string avatar, string contrasena, string nombre, string correo)
        {
            DataTable schema = null;
            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from usuarios", connection);
            cmd.ExecuteNonQuery();
            dataRead = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
            bool banderaRegistro = false;
            schema = dataRead.GetSchemaTable();
           
            if (dataRead.Read() == false)
            {
                cmd = new SqlCommand("insert into usuarios (avatar,password,nombre,correo) values ('" + avatar + "','" + contrasena + "','" + nombre + "','" + correo + "')", connection);
                dataRead.Close();
                cmd.ExecuteScalar();
                banderaRegistro = true;
            }
            else
            {
                while (schema.Rows.Count>0)
                {
                    if (avatar != dataRead["avatar"].ToString() || correo != dataRead["correo"].ToString())
                    {
                        cmd = new SqlCommand("insert into usuarios (avatar,password,nombre,correo) values ('" + avatar + "','" + contrasena + "','" + nombre + "','" + correo + "')", connection);
                        dataRead.Close();
                        cmd.ExecuteScalar();
                        banderaRegistro = true;

                        break;
                    }
                }
            }

            connection.Close();

            return banderaRegistro;
        }
        public Ent_Avatar loginUsuario(string avatar, string contrasena)
        {
            {
                string conex = configuration.GetConnectionString("DefaultConnecctionString");
                SqlConnection connection = new SqlConnection(conex);
                connection.Open();
                SqlCommand cmd = new SqlCommand("select * from usuarios", connection);
                dataRead = cmd.ExecuteReader();
                Ent_Avatar avatar_user = new Ent_Avatar();
                avatar_user.Estado = false;
                while (dataRead.Read())
                {
                    if (avatar == dataRead["avatar"].ToString() && contrasena == dataRead["password"].ToString())
                    {
                        avatar_user.Id = int.Parse(dataRead["id"].ToString());
                        avatar_user.Usuario = dataRead["avatar"].ToString();
                        avatar_user.Password = dataRead["password"].ToString();
                        avatar_user.Nombre = dataRead["nombre"].ToString();
                        avatar_user.Correo = dataRead["correo"].ToString();
                        avatar_user.Estado = true;
                        break;
                    }

                }

                connection.Close();

                return avatar_user;
            }
        }
    }
}
