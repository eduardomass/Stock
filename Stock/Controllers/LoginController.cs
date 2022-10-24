using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Stock.Models;
using System.Security.Claims;

namespace Stock.Controllers
{
    public class LoginController : Controller
    {
        private readonly StockContext _context;

        public LoginController(StockContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string nombre = User.FindFirstValue(ClaimTypes.Name);
            if (nombre == null)
            {

            }
            else
            {
                ViewBag.NombreUsuario = nombre;
            }

            return View();
        }
        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {

            var listaUsuarios = _context.Usuarios.Where(o => o.Nombre == usuario.Nombre &&
            o.Password == usuario.Password).ToList();

            if (listaUsuarios.Count > 0) //Quiero que sea administrado!
            {
                


                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                // El lo que luego obtendré al acceder a User.Identity.Name
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));

                // Se utilizará para la autorización por roles
                if (usuario.Nombre == "Eduardo")
                    identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));
                else
                    identity.AddClaim(new Claim(ClaimTypes.Role, "USUARIO"));

                // Lo utilizaremos para acceder al Id del usuario que se encuentra en el sistema.
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                // Lo utilizaremos cuando querramos mostrar el nombre del usuario logueado en el sistema.
                identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nombre));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                // En este paso se hace el login del usuario al sistema
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal).Wait();


                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                ViewBag.MensajeDeError = "El usuario es incorrecto.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return View("Index");
        }
    }
}
