using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PruebaTecnica.Models
{
    public class MantenimientoArticulo
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["administracion"].ToString();
            con = new SqlConnection(constr);
        }

        public int Alta(Articulo art)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into Articulos(Id,Nombre,Precio,Descripcion,Familia) values (@Id,@Nombre,@Precio,@Descripcion,@Familia)", con);
            comando.Parameters.Add("@Id", SqlDbType.Int);
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@Precio", SqlDbType.Float);
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@Familia", SqlDbType.VarChar);
            comando.Parameters["@Id"].Value = art.Codigo;
            comando.Parameters["@Nombre"].Value = art.Nombre;
            comando.Parameters["@Precio"].Value = art.Precio;
            comando.Parameters["@Descripcion"].Value = art.Descripcion;
            comando.Parameters["@Familia"].Value = art.Familia;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Articulo> RecuperarTodos()
        {
            Conectar();
            List<Articulo> articulos = new List<Articulo>();

            SqlCommand com = new SqlCommand("select Id,Nombre,Precio,Descripcion,Familia from Articulos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Articulo art = new Articulo
                {
                    Codigo = int.Parse(registros["Id"].ToString()),
                    Nombre = registros["Nombre"].ToString(),
                    Precio = float.Parse(registros["Precio"].ToString()),
                    Descripcion = registros["Descripcion"].ToString(),
                    Familia = registros["Familia"].ToString()
                };
                articulos.Add(art);
            }
            con.Close();
            return articulos;
        }

        public Articulo Recuperar(int Id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("select Id,Nombre,Precio,Descripcion,Familia from Articulos where Id=@Id", con);
            comando.Parameters.Add("@Id", SqlDbType.Int);
            comando.Parameters["@Id"].Value = Id;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Articulo articulo = new Articulo();
            if (registros.Read())
            {
                articulo.Codigo = int.Parse(registros["Id"].ToString());
                articulo.Nombre = registros["Nombre"].ToString();
                articulo.Precio = float.Parse(registros["Precio"].ToString());
                articulo.Descripcion = registros["Descripcion"].ToString();
                articulo.Familia = registros["Familia"].ToString();
            }
            con.Close();
            return articulo;
        }
        public int Modificar(Articulo art)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("update Articulos set Nombre=@Nombre,Precio=@Precio,Descripcion=@Descripcion,Familia=@Familia where Id=@Id", con);
            comando.Parameters.Add("@Id", SqlDbType.Int);
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@Precio", SqlDbType.Float);
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@Familia", SqlDbType.VarChar);
            comando.Parameters["@Id"].Value = art.Codigo;
            comando.Parameters["@Nombre"].Value = art.Nombre;
            comando.Parameters["@Precio"].Value = art.Precio;
            comando.Parameters["@Descripcion"].Value = art.Descripcion;
            comando.Parameters["@Familia"].Value = art.Familia;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public int Borrar(int Id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from Articulos where Id=@Id", con);
            comando.Parameters.Add("@Id", SqlDbType.Int);
            comando.Parameters["@Id"].Value = Id;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Articulo> Buscar(string texto)
        {
            Conectar();
            List<Articulo> articulos = new List<Articulo>();

            SqlCommand com = new SqlCommand("select Id,Nombre,Precio,Descripcion,Familia from Articulos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                if (registros["Nombre"].ToString().ToLower().Contains(texto.ToLower()) || registros["Descripcion"].ToString().ToLower().Contains(texto.ToLower()))
                {
                    Articulo art = new Articulo
                    {
                        Codigo = int.Parse(registros["Id"].ToString()),
                        Nombre = registros["Nombre"].ToString(),
                        Precio = float.Parse(registros["Precio"].ToString()),
                        Descripcion = registros["Descripcion"].ToString(),
                        Familia = registros["Familia"].ToString()
                    };
                    articulos.Add(art);
                }



                
            }
            con.Close();
            return articulos;
        }
    }
}