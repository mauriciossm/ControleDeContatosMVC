using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Filters;
using SistamaDeContatos.Models;
using System.Diagnostics;

namespace SistamaDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult PaginaErro()
        {
            return View();
        }
    }
}