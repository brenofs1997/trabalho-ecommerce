using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class CategoriaProdutoController : Controller
    {
    


        
        public IActionResult ObterCategorias()
        {
            Hello.CamadaNegocio.ProdutoCamadaNegocio ccn =
                new CamadaNegocio.ProdutoCamadaNegocio();
            return Json(ccn.ObterCategorias());

        }
        public IActionResult ObterProdutos(string pt)
        {
            Hello.CamadaNegocio.ProdutoCamadaNegocio pcn =
               new CamadaNegocio.ProdutoCamadaNegocio();

            return Json(pcn.ObterProdutos(pt));
        }
        public IActionResult listar(int pt)
        {
            Hello.CamadaNegocio.ProdutoCamadaNegocio pcn =
               new CamadaNegocio.ProdutoCamadaNegocio();

            return Json(pcn.listar(pt));
        }
    }
}