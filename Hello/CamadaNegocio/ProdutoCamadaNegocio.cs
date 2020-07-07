using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaNegocio
{
    public class ProdutoCamadaNegocio
    {
        public (bool, string) Criar(Models.Produto produto)
        {
            string msg = "";
            bool operacao = false;
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();
            if (String.IsNullOrEmpty(produto.Descricao))
            {
                msg = "Descrição deve ser preenchida";
            }
            else if(produto.Quantidade<=0)
            {
                msg = "Quantidade deve ser preenchida";
                
            }
            else if (produto.Valor <= 0)
            {
                msg = "Valor deve ser preenchida";

            }
            if (produto.Categoria==null)
            {
                msg = "Categoria deve ser escolhida";
            }
            else if(produtoBD.Criar(produto)) 
            {

                msg = "Produto cadastrado com sucesso.";
                operacao = true;
            }
            else
            {
                msg = "Erro ao cadastrar produto";
            }
            return (operacao, msg);
        }
        public List<Models.Categoria> ObterCategorias()
        {
            List<Models.Categoria> cts = new List<Models.Categoria>();
            CamadaAcessoDados.CategoriaProdutoBD categoriaProdutoBD = new CamadaAcessoDados.CategoriaProdutoBD();
            cts = categoriaProdutoBD.Buscar("");
            //ir no bd...
                                
            return cts;
        }
        public List<Models.Produto> ObterProdutos(string pd)
        {
            List<Models.Produto> produtos = new List<Models.Produto>();
            //ir no bd...
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();
            produtos = produtoBD.Buscar(pd);
            return produtos;
        }
        public List<byte[]> ObterFotoS(int id)
        {
            CamadaAcessoDados.ProdutoBD ubd = new CamadaAcessoDados.ProdutoBD();
            return ubd.ObterFotoS(id);
        }

        public bool Excluir(int id)
        {
            CamadaAcessoDados.ProdutoBD pbd = new CamadaAcessoDados.ProdutoBD();
            return pbd.Excluir(id);
        }
        public List<Models.Produto> listar(int pd)
        {
            List<Models.Produto> produtos = new List<Models.Produto>();
            //ir no bd...
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();
            produtos = produtoBD.listar(pd);
            return produtos;
        }
        public (bool, string) IncluirFoto(int id, byte[] foto)
        {
            string msg = "";
            bool operacao = false;
            // if (foto.Length > 10 * (1024 * 4))
            // { msg ="Arquivo Muito Grande"; }
            // else
            {
                CamadaAcessoDados.ProdutoBD ubd = new CamadaAcessoDados.ProdutoBD();
                operacao = ubd.IncluirFoto(id, foto);

            }
            return (operacao, msg);
        }
    }
}
