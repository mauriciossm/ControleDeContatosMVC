﻿using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Filters;

namespace SistamaDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
