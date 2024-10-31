using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PublicacionController : Controller
    {
        enum PublicacionView
        {
            Todas,
            Subasta,
            Ventas
        }
        Sistema _sistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.publicaciones = _sistema.Publicaciones;
            ViewBag.publicacionView = PublicacionView.Todas;
            return View();
        }

        [HttpGet]
        public IActionResult MostrarPublicacionVentas()
        {
            ViewBag.publicaciones = _sistema.ObtenerPublicacionVentas();
            ViewBag.publicacionView = PublicacionView.Ventas;
            return View("index");
        }

        [HttpGet]
        public IActionResult MostrarPublicacionSubasta()
        {
            ViewBag.publicaciones = _sistema.ObtenerPublicacionSubasta();
            ViewBag.publicacionView = PublicacionView.Subasta;
            return View("index");
        }
    }
}
