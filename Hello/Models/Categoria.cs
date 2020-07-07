using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Models
{
    public class Categoria
    {
        int _id;
        String _descricao;

        public int Id { get => _id; set => _id = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }

        public Categoria()
        {
            _id = 0;
            _descricao = "";   
        }

        public Categoria( int id)
        {
           
            _id =id;
        }
    }
}
