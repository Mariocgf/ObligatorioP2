using Dominio;
using Dominio.Entidades;
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
        public IActionResult Index(string msj)
        {
            string session = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(session))
            {
                return Redirect("/");
            }
            ViewBag.publicaciones = _sistema.Publicaciones;
            ViewBag.publicacionView = PublicacionView.Todas;
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

        [HttpGet]
        public IActionResult Comprar(int id)
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
                return Redirect("/");
            Venta publicacion = _sistema.ObtenerPublicacionVenta(id);
            Cliente cliente = (Cliente)_sistema.ObtenerUsuario(email);
            try
            {
                _sistema.Comprar(publicacion, cliente);
                HttpContext.Session.SetString("billetera", cliente.Billetera.ToString());
                ViewBag.msj = "success:Compra realizada correctamente";
            }
            catch (Exception error)
            {
                ViewBag.msj = error.Message.Split(":")[1];
            }
            return RedirectToAction("index", new { msj= ViewBag.msj });
        }
    }
}
