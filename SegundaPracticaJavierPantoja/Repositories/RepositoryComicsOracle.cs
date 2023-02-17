using Oracle.ManagedDataAccess.Client;
using SegundaPracticaJavierPantoja.Models;
using System.Data;

namespace SegundaPracticaJavierPantoja.Repositories
{
    #region
   /* CREATE OR REPLACE PROCEDURE SP_INSERTAR_COMIC_ORACLE
        (P_IDCOMIC COMICS.IDCOMIC%TYPE,
        P_NOMBRE COMICS.NOMBRE%TYPE,
        P_IMAGEN COMICS.IMAGEN%TYPE,
        P_DESCRIPCION COMICS.DESCRIPCION%TYPE)
         AS
         BEGIN
            INSERT INTO COMICS VALUES(P_IDCOMIC, P_NOMBRE, P_IMAGEN, P_DESCRIPCION);
         END;*/
    #endregion
    public class RepositoryComicsOracle : IRepositoryComics
    {
        private OracleConnection cn;
        private OracleCommand com;
        private OracleDataAdapter adapter;
        private DataTable tablaComics;

        public RepositoryComicsOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True;User Id=SYSTEM;Password=oracle";

            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            string oracle = "select * from comics";
            this.adapter = new OracleDataAdapter(oracle, connectionString);
            //Creamos la tabla de doctores
            this.tablaComics = new DataTable();
            this.adapter.Fill(this.tablaComics);
        }
        public List<Comic> GetAllComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           select new Comic
                           {
                               IdComic = datos.Field<int>("IDCOMIC"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Imagen = datos.Field<string>("IMAGEN"),
                               Descripcion = datos.Field<string>("DESCRIPCION")
                               
                           };

            return consulta.ToList();
        }

        public void InsertComic(int idComic, string nombre, string imagen, string descripcion)
        {
            OracleParameter pamId = new OracleParameter("@IDCOMIC", idComic);
            this.com.Parameters.Add(pamId);
            OracleParameter pamNombre = new OracleParameter("@NOMBRE", nombre);
            this.com.Parameters.Add(pamNombre);
            OracleParameter pamImagen = new OracleParameter("@IMAGEN", imagen);
            this.com.Parameters.Add(pamImagen);
            OracleParameter pamDescripcion = new OracleParameter("DESCRIPCION", descripcion);
            this.com.Parameters.Add(pamDescripcion);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERTAR_COMIC_ORACLE";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
