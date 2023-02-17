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
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetAllComics();
            return View(comics);
        }

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
