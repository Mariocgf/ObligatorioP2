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
            string session = HttpContext.Session.GetString("sesion");
            if (!string.IsNullOrEmpty(session))
                return Redirect("/publicacion");
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string contrasenia)
        {
            try
            {
                if (_sistema.Login(email, contrasenia))
                {
                    HttpContext.Session.SetString("sesion", email);
                }
                return Redirect("/publicacion");
            }
            catch (Exception error)
            {
                ViewBag.msj = error.Message.Split(":")[1];
            }

            return View("index");
        }
    }
}
