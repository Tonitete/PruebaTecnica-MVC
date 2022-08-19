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
            SqlCommand comando = new SqlCommand("insert into articulos(codigo,nombre,precio,descripcion,familia) values (@codigo,@nombre,@precio,@descripcion,@familia)", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@precio", SqlDbType.Float);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@familia", SqlDbType.VarChar);
            comando.Parameters["@codigo"].Value = art.Codigo;
            comando.Parameters["@nombre"].Value = art.Nombre;
            comando.Parameters["@precio"].Value = art.Precio;
            comando.Parameters["@descripcion"].Value = art.Descripcion;
            comando.Parameters["@familia"].Value = art.Familia;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Articulo> RecuperarTodos()
        {
            Conectar();
            List<Articulo> articulos = new List<Articulo>();

            SqlCommand com = new SqlCommand("select codigo,nombre,precio,descripcion,familia from articulos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Articulo art = new Articulo
                {
                    Codigo = int.Parse(registros["codigo"].ToString()),
                    Nombre = registros["nombre"].ToString(),
                    Precio = float.Parse(registros["precio"].ToString()),
                    Descripcion = registros["descripcion"].ToString(),
                    Familia = registros["familia"].ToString()
                };
                articulos.Add(art);
            }
            con.Close();
            return articulos;
        }

        public Articulo Recuperar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("select codigo,nombre,precio,descripcion,familia from articulos where codigo=@codigo", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Articulo articulo = new Articulo();
            if (registros.Read())
            {
                articulo.Codigo = int.Parse(registros["codigo"].ToString());
                articulo.Nombre = registros["nombre"].ToString();
                articulo.Precio = float.Parse(registros["precio"].ToString());
                articulo.Descripcion = registros["descripcion"].ToString();
                articulo.Familia = registros["familia"].ToString();
            }
            con.Close();
            return articulo;
        }
        public int Modificar(Articulo art)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("update articulos set nombre=@nombre,precio=@precio,descripcion=@descripcion,familia=@familia where codigo=@codigo", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters.Add("@precio", SqlDbType.Float);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@familia", SqlDbType.VarChar);
            comando.Parameters["@codigo"].Value = art.Codigo;
            comando.Parameters["@nombre"].Value = art.Nombre;
            comando.Parameters["@precio"].Value = art.Precio;
            comando.Parameters["@descripcion"].Value = art.Descripcion;
            comando.Parameters["@familia"].Value = art.Familia;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from articulos where codigo=@codigo", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Articulo> Buscar(string texto)
        {
            Conectar();
            List<Articulo> articulos = new List<Articulo>();

            SqlCommand com = new SqlCommand("select codigo,nombre,precio,descripcion,familia from articulos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                if (registros["nombre"].ToString().ToLower().Contains(texto.ToLower()) || registros["descripcion"].ToString().ToLower().Contains(texto.ToLower()))
                {
                    Articulo art = new Articulo
                    {
                        Codigo = int.Parse(registros["codigo"].ToString()),
                        Nombre = registros["nombre"].ToString(),
                        Precio = float.Parse(registros["precio"].ToString()),
                        Descripcion = registros["descripcion"].ToString(),
                        Familia = registros["familia"].ToString()
                    };
                    articulos.Add(art);
                }



                
            }
            con.Close();
            return articulos;
        }
    }
}