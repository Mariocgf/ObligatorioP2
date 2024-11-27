using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    [Login]
    public class PublicacionController : Controller
    {
        enum PublicacionView
        {
            Todas,
            Subasta,
            Ventas
        }
        Sistema _sistema = Sistema.Instancia;
        [Client]
        [HttpGet]
        public IActionResult Index(string msj)
        {
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
        [Client]
        [HttpGet]
        public IActionResult MostrarPublicacionVentas()
        {
            ViewBag.publicaciones = _sistema.ObtenerPublicacionesVentas();
            ViewBag.publicacionView = PublicacionView.Ventas;
            return View("index");
        }

        [HttpGet]
        public IActionResult MostrarPublicacionSubasta()
        {
            ViewBag.publicaciones = _sistema.ObtenerPublicacionesSubasta();
            ViewBag.publicacionView = PublicacionView.Subasta;
            return View("index");
        }

        [Client]
        [HttpGet]
        public IActionResult Comprar(int id)
        {
            string email = HttpContext.Session.GetString("email");
            Cliente cliente = (Cliente)_sistema.ObtenerUsuario(email);
            try
            {
                _sistema.Comprar(id, email);
                HttpContext.Session.SetString("billetera", cliente.Billetera.ToString());
                ViewBag.msj = "success:Compra realizada correctamente";
            }
            catch (Exception error)
            {
                ViewBag.msj = error.Message.Split(":")[1];
            }
            return RedirectToAction("index", new { ViewBag.msj });
        }

        [Client]
        [HttpPost]
        public IActionResult Ofertar(int id, decimal monto)
        {
            string email = HttpContext.Session.GetString("email");
            Cliente cliente = (Cliente)_sistema.ObtenerUsuario(email);
            try
            {
                _sistema.Ofertar(id, cliente, monto);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.msj = e.Message;
            }
            return RedirectToAction("index", new { ViewBag.msj });
        }

        [HttpGet]
        [Admin]
        public IActionResult Editar(int id)
        {
            Publicacion publicacion = _sistema.ObtenerPublicacion(id);
            if (publicacion is Venta)
                ViewBag.publicacion = (Venta)publicacion;
            if (publicacion is Subasta)
                ViewBag.publicacion = (Subasta)publicacion;
            return View();
        }

        [HttpGet]
        [Admin]
        public IActionResult Finalizar(int id)
        {
            Usuario finalizador = _sistema.ObtenerUsuario(HttpContext.Session.GetString("email"));
            _sistema.FinalizarSubasta(id, finalizador);
            return Redirect("/publicacion");
        }
    }
}
