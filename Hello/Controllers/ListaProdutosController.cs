using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class ListaProdutosController : Controller
    {
        public IActionResult ListaProdutos()
        {
            return View();
        }
    }
}