using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        enum Tipo
        {
            Iniciar,
            Registrar
        }
        Sistema _sistema = Sistema.Instancia;
        [HttpGet]
        public IActionResult Index(string msj)
        {
            string session = HttpContext.Session.GetString("email");
            if (!string.IsNullOrEmpty(session))
                return Redirect("/publicacion");
            ViewBag.tipo = Tipo.Iniciar;
            if (!string.IsNullOrEmpty(msj))
            {
                string[] values = msj.Split(':');
                if (values.Length > 1)
                {
                    ViewBag.msjTipo = values[0];
                    ViewBag.msj = values[1];
                }
                else
                {
                    ViewBag.msj = msj;
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string contrasenia)
        {
            try
            {
                if (_sistema.Login(email, contrasenia))
                    HttpContext.Session.SetString("email", email);
                Usuario aux = _sistema.ObtenerUsuario(email);
                if (aux is Cliente)
                {
                    Cliente cliente = (Cliente)aux;
                    var (nombre, apellido,correo, billetera) = cliente;
                    HttpContext.Session.SetString("nombre", nombre);
                    HttpContext.Session.SetString("apellido", apellido);
                    HttpContext.Session.SetString("billetera", billetera.ToString());
                }
                return Redirect("/publicacion");
            }
            catch (Exception error)
            {
                ViewBag.msj = error.Message.Split(":")[1];
                ViewBag.tipo = Tipo.Iniciar;
            }
            return View("index");
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            string session = HttpContext.Session.GetString("email");
            if (!string.IsNullOrEmpty(session))
                return Redirect("/publicacion");
            ViewBag.tipo = Tipo.Registrar;
            return View("index", new Cliente());
        }

        [HttpPost]
        public IActionResult Registrar(Cliente cliente)
        {
            try
            {
                _sistema.AgregarCliente(cliente);
                return RedirectToAction("index", new { msj = "success:Registro exitoso." });
            }
            catch (Exception error)
            {
                ViewBag.msj = error.Message.Split(":")[1];
            }
            ViewBag.tipo = Tipo.Registrar;

            return View("index", cliente);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
