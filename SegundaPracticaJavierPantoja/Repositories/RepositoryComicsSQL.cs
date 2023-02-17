using Microsoft.Data.SqlClient;
using SegundaPracticaJavierPantoja.Models;
using System.Data;

namespace SegundaPracticaJavierPantoja.Repositories
{
    #region
    /*CREATE PROCEDURE SP_INSERTAR_COMIC
        (@IDSERIE INT, @NOMBRE NVARCHAR(40), @IMAGEN NVARCHAR(50), @DESCRIPCION NVARCHAR(100))
        AS
            INSERT INTO COMICS VALUES(@IDSERIE, @NOMBRE, @IMAGEN, @DESCRIPCION)
        GO*/
    #endregion
    public class RepositoryComicsSQL : IRepositoryComics
    {
        private SqlConnection cn;
        private SqlCommand com;

        private DataTable tablaComics;

        public RepositoryComicsSQL()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2022;TrustServerCertificate=True";
            string sql = "SELECT * FROM COMICS";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaComics = new DataTable();
            adapter.Fill(this.tablaComics);
            ////////////////////////////////
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Comic> GetAllComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           select datos;
            List<Comic> comics = new List<Comic>();
            //VAMOS A RECORRER TODOS LOS DATOS DE LA CONSULTA Y EXTRAERLOS
            foreach (var row in consulta)
            {
                Comic comic = new Comic
                {
                    IdComic = row.Field<int>("IDCOMIC"),
                    Nombre = row.Field<string>("NOMBRE"),
                    Imagen = row.Field<string>("IMAGEN"),
                    Descripcion = row.Field<string>("DESCRIPCION")
                    
                };
                comics.Add(comic);
            }
            return comics;
        }

        public void InsertComic(int idComic, string nombre, string imagen, string descripcion)
        {
            SqlParameter pamId = new SqlParameter("@IDCOMIC", idComic);
            this.com.Parameters.Add(pamId);
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);
            SqlParameter pamDescripcion = new SqlParameter("DESCRIPCION", descripcion);
            this.com.Parameters.Add(pamDescripcion);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERTAR_COMIC";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
    }
}
