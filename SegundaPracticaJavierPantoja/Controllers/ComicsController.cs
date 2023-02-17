using Microsoft.AspNetCore.Mvc;
using SegundaPracticaJavierPantoja.Models;
using SegundaPracticaJavierPantoja.Repositories;

namespace SegundaPracticaJavierPantoja.Controllers
{
    public class ComicsController : Controller
    {
        private IRepositoryComics repo;

        public ComicsController(IRepositoryComics repo)
        {
            //Inicializamos el repositorio
            this.repo = repo;
        }

        public IActionResult Index()
        {
            //Metemos la lista de comics en el index
            List<Comic> comics = this.repo.GetAllComics();
            //Devolvemos los comics a la vista para ser vistos
            return View(comics);
        }

        //Metodo p
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Comic com)
        {
            this.repo.InsertComic( com.Nombre, com.Imagen, com.Descripcion);
            return RedirectToAction("Index");

        }
    }
}
