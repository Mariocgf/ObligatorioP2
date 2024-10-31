using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema _sistema = Sistema.Instancia;
        [HttpGet]
        public IActionResult Index()
        {
            // Puede retornar vista - redireccion - json
            //ViewBag.Usuario = _sistema.Usuarios;
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {

            return View();
        }
    }
}
