using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaAcessoDados
{
    public class CategoriaProdutoBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();
        public Models.Categoria Obter(int codigo)
        {
            String sql = "";
            Models.Categoria ObterCategoria = new Models.Categoria();
            var parametros = _bd.GerarParametros();
            sql = "select * from categoria where idcategoria = @codigo";

            parametros.Add("@codigo", +codigo);
            DataTable tabela = _bd.ExecutarSelect(sql, parametros);

            if(tabela.Rows.Count==1)
                ObterCategoria=Map(tabela.Rows[0]);
            return ObterCategoria;
        }
        public List<Models.Categoria> Buscar(String descricao)
        {
            String sql = "";
            List<Models.Categoria> listaCategorias = new List<Models.Categoria>();
            var parametros = _bd.GerarParametros();
            if (descricao == ""|| descricao == " ")
                sql = "select * from categoria";
            else
            {
                sql = "select * from categoria where descricao like @descricao";
            }
            parametros.Add("@descricao", "%" + descricao + "%");
            DataTable tabela = _bd.ExecutarSelect(sql,parametros);

            foreach (DataRow row in tabela.Rows)
                listaCategorias.Add(Map(row));
            return listaCategorias;
         }
    
        internal Models.Categoria Map(DataRow row)
        {

            Models.Categoria categoria = new Models.Categoria();
            categoria.Id = Convert.ToInt32(row["idcategoria"]);
            categoria.Descricao = row["descricao"].ToString();

            return categoria;
        }

    }
}
