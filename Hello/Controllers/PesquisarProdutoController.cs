using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class PesquisarProdutoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pesquisar(string nome)
        {
            Hello.CamadaNegocio.ProdutoCamadaNegocio pcn =
               new CamadaNegocio.ProdutoCamadaNegocio();

            return Json(pcn.ObterProdutos(nome));
        }
    }
}