using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Criar([FromBody]Dictionary<string, string> dados)
        {
            bool operacao = false;
            int senhaNumero;
            string msg="";
            if (!int.TryParse(dados["senha"], out senhaNumero))
            {
                msg = "Digite apenas Numeros";
            }
            else
            {
                Models.Usuario usuario = new Models.Usuario();
                usuario.NomeUsuario = dados["nomeUsuario"];
                usuario.Senha =dados["senha"];
                CamadaNegocio.UsuarioCamadaNegocio ucn = new CamadaNegocio.UsuarioCamadaNegocio();
                (operacao, msg)=ucn.Criar(usuario);
               
            }
            return Json(new
            {
                operacao,msg
            }) ;
        }
    }
}