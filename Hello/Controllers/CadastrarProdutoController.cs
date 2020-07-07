using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class CadastrarProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Criar([FromBody] Dictionary<string,string> dados)
        {
            bool operacao = false;
            string msg = "";
            int qtde = 0;
            Double valorprod = 0.0;
            Models.Produto produto = new Models.Produto();
            if (String.IsNullOrEmpty(dados["descricao"]))
            {
                msg = "Descrição não pode ser vazia";
            }
             if (!int.TryParse(dados["quantidade"],out qtde))
            {
                msg = "Digite apenas Numeros";
            }
             if (!Double.TryParse(dados["valor"], out valorprod))
            {
                msg = "Digite apenas Numeros";
            }
            else
            {
               
                
                produto.Descricao = dados["descricao"];
                produto.Quantidade = Convert.ToInt32(dados["quantidade"]);
                produto.Valor = Convert.ToDouble(dados["valor"]);
                produto.Categoria = new Models.Categoria(Convert.ToInt32(dados["categoria"]));

                CamadaNegocio.ProdutoCamadaNegocio
                    ucn = new CamadaNegocio.ProdutoCamadaNegocio();
                (operacao, msg) = ucn.Criar(produto);
            }
            return Json(new
            {
                id = produto.Id,
                operacao,
                msg
            });
        }
        public IActionResult Foto()
        {
            bool operacao = false;
            string msg = "";
            string nome = "";

            int id = Convert.ToInt32(Request.Form["id"]);
            for (var i = 0; i < Request.Form.Files.Count; i++)
            {
                nome = Request.Form.Files[i].FileName;
                if (System.IO.Path.GetExtension(nome) != ".jpg")
                {
                    msg = "Formato de foto inválido.";
                }
                else
                {
                    //FileStream fs = new FileStream(@"c:\\zssss.jpg", FileMode.Create);
                    //Request.Form.Files[0].CopyTo(fs);

                    MemoryStream ms = new MemoryStream();
                    Request.Form.Files[i].CopyTo(ms);
                    byte[] arq = ms.ToArray();

                    CamadaNegocio.ProdutoCamadaNegocio
                        ucn = new CamadaNegocio.ProdutoCamadaNegocio();
                    (operacao, msg) = ucn.IncluirFoto( id, arq);

                }

            }
            //string nome = Request.Form.Files[0].FileName;

            if (operacao == false)
                msg = "Produto nao cadastrado.";

            return Json(new
            {

                operacao,
                msg
            });


        }
        public IActionResult ObterFotoS(int id, int pos)
        {
            CamadaNegocio.ProdutoCamadaNegocio ucn = new CamadaNegocio.ProdutoCamadaNegocio();

            List<byte[]> foto = ucn.ObterFotoS(id);


           // if (foto == null)
          //      return File("~/imgs/semfoto.jpg", "image/jpg");
           // else
               return File(foto[pos], "image/jpg");
        }
        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.ProdutoCamadaNegocio pcn = new CamadaNegocio.ProdutoCamadaNegocio();
            bool operacao = pcn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }
    }
}