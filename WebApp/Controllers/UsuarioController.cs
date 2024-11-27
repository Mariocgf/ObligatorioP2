using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    [Login]
    [Client]
    public class UsuarioController : Controller
    {
        Sistema _sistema = Sistema.Instancia;
        [HttpGet]
        public IActionResult Index(string msj)
        {
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
        [HttpGet]
        [HttpPost]
        public IActionResult Depositar(decimal monto)
        {
            string email = HttpContext.Session.GetString("email");
            string rol = HttpContext.Session.GetString("rol");
            if (rol == "ADMIN")
                return Redirect("/");
            try
            {
                Cliente cliente = (Cliente)_sistema.ObtenerUsuario(email);
                cliente.Depositar(monto);
                HttpContext.Session.SetString("billetera", cliente.Billetera.ToString());
                ViewBag.msj = "success:Deposito realizado correctamente";
            }
            catch (Exception error)
            {
                ViewBag.msj = error.Message.Split(":")[1];
            }
            return RedirectToAction("index", new { msj = ViewBag.msj });
        }

    }
}
