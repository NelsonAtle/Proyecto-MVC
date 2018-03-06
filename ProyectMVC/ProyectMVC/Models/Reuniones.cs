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
    public class Reuniones
    {
        private IConfiguration configuration;

        private SqlDataReader dataRead = null;
        public Reuniones(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void agregarReunion(string titulo, DateTime dia, DateTime hora, int[] usuarios,bool linea,int cliente)
        {
            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();

            int en_linea = 0;
            if (linea)
            {
                en_linea = 1;
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO reuniones(titulo,fecha,hora,virtual,id_cliente) values('"+titulo+"','"+dia+"','"+hora+"',"+en_linea+","+cliente+"); " +
                                            "SELECT SCOPE_IDENTITY();", connection);
            
            int id_reunion = Convert.ToInt32(cmd.ExecuteScalar());

            for (int i = 0; i < usuarios.Length; i++)
            {
                cmd = new SqlCommand("INSERT INTO reunion_usuarios(id_usuario,id_reunion) values(" + Convert.ToInt32(usuarios[i].ToString())+","+id_reunion+")", connection);
                cmd.ExecuteScalar();
            }
            connection.Close();
            
        }
        
        public IList<Ent_Reunion> cargarReuniones()
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from reunion_usuarios", connection);
            dataRead = cmd.ExecuteReader();
            IList<Ent_Reu_Usuarios> listaUsuarios = new List<Ent_Reu_Usuarios>();
            while (dataRead.Read())
            {
                listaUsuarios.Add(new Ent_Reu_Usuarios() {
                    Id_reunion = Convert.ToInt32(dataRead["id_reunion"].ToString()),
                    Id_usuario=Convert.ToInt32(dataRead["id_usuario"].ToString()) 
                });
            }
           
            dataRead.Close();
            cmd = new SqlCommand("select * from reuniones", connection);
            dataRead = cmd.ExecuteReader();

            
            IList<Ent_Reunion> listaReuniones = new List<Ent_Reunion>();
            while (dataRead.Read())
            {
                listaReuniones.Add(new Ent_Reunion()
                {
                    Id = Convert.ToInt32(dataRead["id"].ToString()),
                    Titulo = dataRead["titulo"].ToString(),
                    Dia = dataRead["fecha"].ToString(),
                    Tiempo =dataRead["hora"].ToString(),
                    Usuarios = listaUsuarios,
                    Linea = Convert.ToInt32(dataRead["virtual"].ToString()),
                    Cliente = Convert.ToInt32(dataRead["id_cliente"].ToString())

                });
            }
            dataRead.Close();
            connection.Close();
            return listaReuniones;
        }
        //segunda parte
        public void actualizarReunion(string titulo, DateTime dia, DateTime hora, int[] usuarios, bool linea, int cliente,int id)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();

            int en_linea = 0;
            if (linea)
            {
                en_linea = 1;
            }
            SqlCommand cmd = new SqlCommand("update reuniones set titulo='" + titulo + "'," +
                                            "fecha='" + dia + "'," +
                                            "hora='" + hora + "'," +
                                            "virtual='" + en_linea + "'," +
                                            "id_cliente='" + cliente + "' where id="+id, connection);

            cmd.ExecuteScalar();
            /*----------------------------------*/

            cmd = new SqlCommand("select * from reunion_usuarios where id_reunion ="+id, connection);
            dataRead = cmd.ExecuteReader();
            IList<Ent_Reu_Usuarios> listaRegistros = new List<Ent_Reu_Usuarios>();
            while (dataRead.Read())
            {
                listaRegistros.Add(new Ent_Reu_Usuarios()
                {
                    Id = Convert.ToInt32(dataRead["id"].ToString()),
                    Id_reunion = Convert.ToInt32(dataRead["id_reunion"].ToString()),
                    Id_usuario = Convert.ToInt32(dataRead["id_usuario"].ToString())
                });
            }

            dataRead.Close();
            /*----------------------------------*/
            
            cmd = new SqlCommand("delete from Reunion_Usuarios where id_reunion = " + id, connection);
            cmd.ExecuteScalar();

            for (int i = 0; i < usuarios.Length; i++)
            {
                cmd = new SqlCommand("INSERT INTO reunion_usuarios(id_usuario,id_reunion) values(" + Convert.ToInt32(usuarios[i].ToString()) + "," + id + ")", connection);
                cmd.ExecuteScalar();
            }
            connection.Close();

        }
        public void eliminarReunion(int id)
        {

            string conex = configuration.GetConnectionString("DefaultConnecctionString");
            SqlConnection connection = new SqlConnection(conex);
            connection.Open();

            SqlCommand cmd = new SqlCommand("delete from Reunion_Usuarios where id_reunion = " + id, connection);
            cmd.ExecuteScalar();

            cmd = new SqlCommand("delete from reuniones where id = "+id , connection);

            cmd.ExecuteScalar();

            connection.Close();

        }
    }
}
