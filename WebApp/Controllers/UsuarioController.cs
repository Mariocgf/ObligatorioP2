using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema _sistema = new Sistema();
        public IActionResult Index()
        {
            // Puede retornar vista - redireccion - json
            ViewBag.Usuario = _sistema.Usuarios;
            return View();
        }

        public IActionResult Ver(int id)
        {
            ViewBag.Usuario = _sistema.ObtenerUsuario(id);
            return View();
        }
    }
}
