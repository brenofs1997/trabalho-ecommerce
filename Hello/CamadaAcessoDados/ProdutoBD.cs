using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaAcessoDados
{
    public class ProdutoBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        public bool Criar(Models.Produto produto)
        {
            //mapeamento Objeto-Relacional (ORM);

            string insert = @"insert into produto(descricao,valor,quantidade,idcategoria)
                              values(@descricao,@valor,@quantidade,@idcategoria)";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descricao", produto.Descricao);
            parametros.Add("@valor", produto.Valor);
            parametros.Add("@quantidade", produto.Quantidade);
            parametros.Add("@idcategoria",produto.Categoria.Id);
            int linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
            if (linhasAfetadas > 0)
            {
                produto.Id = _bd.UltimoId;
            }
            return linhasAfetadas > 0;
        }
        public bool Excluir(int id)
        {
            string sql = @"delete from produto where idproduto = " + id;

            return _bd.ExecutarNonQuery(sql) > 0;

        }
        public List<Models.Produto> Buscar(String descricao)
        {
            String sql = "";
            List<Models.Produto> listarProdutos = new List<Models.Produto>();
            var parametros = _bd.GerarParametros();
            if (descricao == "" || descricao == " ")
                sql = "select * from produto";
            else
            {
                sql = "select * from produto where descricao like @descricao";
            }
            parametros.Add("@descricao", "%" + descricao + "%");
            DataTable tabela = _bd.ExecutarSelect(sql, parametros);

            foreach (DataRow row in tabela.Rows)
                listarProdutos.Add(Map(row));
            return listarProdutos;
        }
        public bool IncluirFoto(int id, byte[] foto)
        {
            //mapeamento Objeto-Relacional (ORM);

            string insert = @"insert into imagensproduto(idproduto,foto) 
                             values(@idproduto,@foto)";

            var parametros = _bd.GerarParametros();

            parametros.Add("@idproduto", id);
            var parametrosBinarios = _bd.GerarParametrosBinarios();
            parametrosBinarios.Add("@foto", foto);
            int linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros, parametrosBinarios);

            return linhasAfetadas > 0;
        }
        public List<Models.Produto> listar(int descricao)
        {
            String sql = "";
            List<Models.Produto> listarProdutos = new List<Models.Produto>();
            var parametros = _bd.GerarParametros();
            if (descricao==0)
                sql = "select * from produto";
            else
            {
                sql = "select * from produto where idcategoria = @descricao";
            }
            parametros.Add("@descricao", + descricao);
            DataTable tabela = _bd.ExecutarSelect(sql, parametros);

            foreach (DataRow row in tabela.Rows)
                listarProdutos.Add(Map(row));
            return listarProdutos;
        }
        public List<Models.Produto> Buscar(int codigo)
        {
            String sql = "";
            List<Models.Produto> listarProdutos = new List<Models.Produto>();
            var parametros = _bd.GerarParametros();
         
                sql = "select * from produto where idproduto =@codigo";
            
            parametros.Add("@codigo",  + codigo);
            DataTable tabela = _bd.ExecutarSelect(sql, parametros);

            foreach (DataRow row in tabela.Rows)
                listarProdutos.Add(Map(row));
            return listarProdutos;
        }
        public List<byte[]> ObterFotoS(int id)
        {
            List<byte[]> retornos = new List<byte[]>();
            byte[] retorno = null;

            string select = @"select foto
                              from imagensproduto 
                              where idproduto = " + id;

            object fotoBd = _bd.ExecutarScalar(select);
            DataTable dt = _bd.ExecutarScalarF(select);
            foreach (DataRow row in dt.Rows)
            {
                retornos.Add((byte[])row["foto"]);
            }

            return retornos;

        }
        public int qtdeFoto(int id)
        {
            string select = @"select * 
                              from imagensproduto 
                              where idproduto = " + id;
            DataTable dt = _bd.ExecutarSelect(select);

            return dt.Rows.Count;

        }
        internal Models.Produto Map(DataRow row)
        {

            Models.Produto produto = new Models.Produto();
            produto.Id = Convert.ToInt32(row["idproduto"]);
            produto.Descricao = row["descricao"].ToString();
            produto.Valor= Convert.ToDouble(row["valor"].ToString());
            produto.QtdeFoto = qtdeFoto(Convert.ToInt32(row["idproduto"]));
            return produto;
        }

    }
}
