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
        enum Rol
        {
            ADMIN,
            CLIENTE
        }
        Sistema _sistema = Sistema.Instancia;
        [HttpGet]
        public IActionResult Index(string msj)
        {
            string? session = HttpContext.Session.GetString("email");
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
                var (nombre, apellido) = aux;
                HttpContext.Session.SetString("nombre", nombre);
                HttpContext.Session.SetString("apellido", apellido);
                if (aux is Cliente cliente)
                {
                    decimal billetera = cliente.Billetera;
                    HttpContext.Session.SetString("billetera", billetera.ToString());
                    HttpContext.Session.SetString("rol", Rol.CLIENTE.ToString());
                }
                if (aux is Administrador)
                    HttpContext.Session.SetString("rol", Rol.ADMIN.ToString());
                return Redirect("/publicacion");
            }
            catch (Exception )
            {
                ViewBag.msj = "Credenciales incorrectas.";
                ViewBag.tipo = Tipo.Iniciar;
            }
            return View("index");
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            string? session = HttpContext.Session.GetString("email");
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
