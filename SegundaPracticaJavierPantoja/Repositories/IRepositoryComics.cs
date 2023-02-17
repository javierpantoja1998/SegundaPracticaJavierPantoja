using SegundaPracticaJavierPantoja.Models;

namespace SegundaPracticaJavierPantoja.Repositories
{
    public interface IRepositoryComics
    {
        //Metodo para sacar la lista de comics
        List<Comic> GetAllComics();

        void InsertComic(int idSerie, string nombre, string imagen, string descripcion);


    }
}
