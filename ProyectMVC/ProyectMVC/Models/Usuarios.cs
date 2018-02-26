using Microsoft.Extensions.Configuration;
using ProyectMVC.Entidades;
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
                cmd = new SqlCommand("insert into usuarios (avatar,password,nombre,correo,tipo) values ('" + avatar + "','" + contrasena + "','" + nombre + "','" + correo + "','Cliente')", connection);
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
        public Ent_Usuario loginUsuario(string avatar, string contrasena)
        {
            {
                string conex = configuration.GetConnectionString("DefaultConnecctionString");
                SqlConnection connection = new SqlConnection(conex);
                connection.Open();
                SqlCommand cmd = new SqlCommand("select * from usuarios", connection);
                dataRead = cmd.ExecuteReader();
                Ent_Usuario avatar_user = new Ent_Usuario();
                avatar_user.Estado = 0;
                while (dataRead.Read())
                {
                    if (avatar == dataRead["avatar"].ToString() && contrasena == dataRead["password"].ToString())
                    {
                        avatar_user.Id = int.Parse(dataRead["id"].ToString());
                        avatar_user.Avatar = dataRead["avatar"].ToString();
                        avatar_user.Password = dataRead["password"].ToString();
                        avatar_user.Nombre = dataRead["nombre"].ToString();
                        avatar_user.Correo = dataRead["correo"].ToString();
                        avatar_user.Tipo = dataRead["tipo"].ToString();
                        avatar_user.Estado = 1;
                        break;
                    }

                }

                connection.Close();

                return avatar_user;
            }
        }
        public IList<Ent_Usuario> cargarUsuarios()
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from usuarios", connection);
            dataRead = cmd.ExecuteReader();

            // schema = dataRead.GetSchemaTable();
            IList<Ent_Usuario> listaUsuarios = new List<Ent_Usuario>();
            while (dataRead.Read())
            {
                listaUsuarios.Add(new Ent_Usuario()
                {
                    Id = int.Parse(dataRead["id"].ToString()),
                    Nombre = dataRead["nombre"].ToString(),
                    Password = dataRead["password"].ToString(),
                    Avatar = dataRead["avatar"].ToString(),
                    Correo = dataRead["correo"].ToString(),
                    Tipo =   dataRead["tipo"].ToString()
                    
                });
            }
            dataRead.Close();
            connection.Close();
            return listaUsuarios;
        }
        public void actualizarUsuario(string avatar, string contrasena, string nombre, string correo,int id_user)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("update usuarios set nombre='" + nombre + "'," +
                                            "avatar='" + avatar + "'," +
                                            "password='" + contrasena + "'," +
                                            "correo='" + correo + "' where id='" + id_user + "'", connection);
            // dataRead.Close();
            cmd.ExecuteScalar();

            connection.Close();

        }
        public void eliminarUsuario(int id_user)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();


            SqlCommand cmd = new SqlCommand("DELETE FROM usuarios where id='" + id_user + "'", connection);

            cmd.ExecuteScalar();

            connection.Close();

        }
    }
}
