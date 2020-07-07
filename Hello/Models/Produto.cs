using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Models
{
    public class Produto
    {
        int _id;
        double _valor;
        String _descricao;
        Categoria _categoria;
        int _quantidade;
        int _qtdeFoto;
        public int Id { get => _id; set => _id = value; }
        public double Valor { get => _valor; set => _valor = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public Categoria Categoria { get => _categoria; set => _categoria = value; }
        public int Quantidade { get => _quantidade; set => _quantidade = value; }
        public int QtdeFoto { get => _qtdeFoto; set => _qtdeFoto = value; }
        public Produto()
        {
            _id = 0;
            _descricao = "";
            _valor = 0.0;
            _categoria = new Categoria();
            _quantidade = 0;
            _qtdeFoto = 0;
        }

        public Produto(int id,int quantidade, double valor, string descricao,int qtdeFoto, Categoria categoria)
        {
            _id = id;
            _valor = valor;
            _descricao = descricao;
            _categoria = categoria;
            _quantidade = quantidade;
            _qtdeFoto = qtdeFoto;
        }

    }
}
