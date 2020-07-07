using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hello.Controllers
{
    
    public class CadastrarUsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            int senhaNumero;
            string msg = "";
            Models.Usuario usuario = new Models.Usuario();
            if (dados["nomeUsuario"] == "")
            {
                msg = "Nome não informado.";
            }
            else if (dados["senha"] == "")
            {
                msg = "Senha inválida. Digite apenas número.";
            }
            if (dados["senha"] != dados["senhaConf"])
            {
                msg = "Senhas diferentes.";
            }
            else
            {
               
                usuario.NomeUsuario = dados["nomeUsuario"];
                usuario.Senha = dados["senha"];

                CamadaNegocio.UsuarioCamadaNegocio
                    ucn = new CamadaNegocio.UsuarioCamadaNegocio();
                (operacao, msg) = ucn.Criar(usuario);
            }

            return Json(new
            {
                id=usuario.Id,
                operacao,
                msg
            });

        }
        //[HttpPost]
        public IActionResult Foto()
        {
            bool operacao = false;
            string msg = "";
            long tamMax = 100 * (1024 * 4);
            int id = Convert.ToInt32(Request.Form["id"]);
            string nome = Request.Form.Files[0].FileName;
            if (System.IO.Path.GetExtension(nome) != ".jpg")
            {
                msg = "Formato invalido";
            }
            else if (Request.Form.Files[0].Length > tamMax)
            {
                msg = "Tamanho excedido";

            }
            else
            {
                // FileStream ms = new FileStream("c:\\zsss.jpg",FileMode.Create);
                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);
                byte[] arq = ms.ToArray();
                CamadaNegocio.UsuarioCamadaNegocio
                    ucn = new CamadaNegocio.UsuarioCamadaNegocio();
                (operacao, msg) = ucn.IncluirFoto(id,arq);
            }

            return Json(new{ operacao, msg });
        }

    }
}
