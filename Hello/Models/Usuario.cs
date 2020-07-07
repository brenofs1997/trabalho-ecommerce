using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Models
{
    public class Usuario
    { 
        //poco
        //_camelCase
        int _id;
        string _nomeUsuario;
        string _senha;

        public int Id { get => _id; set => _id = value; }
        public string NomeUsuario { get => _nomeUsuario; set => _nomeUsuario = value; }
        public string Senha { get => _senha; set => _senha = value; }

        public Usuario()
        {
            _id = 0;
            _nomeUsuario = "";
            _senha = "";
        }

        public Usuario(int id,string nomeUsuario,string senha)
        {
            _id = id;
            _nomeUsuario = nomeUsuario;
            _senha = senha;
        }
    }
}
