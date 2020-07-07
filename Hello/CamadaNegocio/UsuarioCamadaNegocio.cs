using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaNegocio
{
    public class UsuarioCamadaNegocio
    {
        public (bool,string) Criar(Models.Usuario usuario)
        {
            string msg = "";
            bool operacao = false;
            CamadaAcessoDados.UsuarioBD usuarioBD = new CamadaAcessoDados.UsuarioBD();
            //obrigatorio nome de usuario unico
            // senha com min 6 caracteres
            if (usuario.Senha.ToString().Length < 6)
            {
                msg = "Senha muito pequena";
            }
            else if (!usuarioBD.validarLogin(usuario.NomeUsuario))
            {
                msg = "Login já cadastrado.";
            }
            else if (usuarioBD.Criar(usuario))
            {
                msg = "Usuário cadastrado com sucesso.";
                operacao = true;
            }
            else
            {
                msg = "Erro ao cadastrar usuário";
            }
            return (operacao, msg);
        }
        public (bool, Models.Usuario) Validar(string usuarioNome, string senha)
        {
            CamadaAcessoDados.UsuarioBD usuarioBD = new CamadaAcessoDados.UsuarioBD();
            return usuarioBD.Validar(usuarioNome, senha);
        }
        public (bool, string) IncluirFoto(int id, byte[] foto)
        {
            string msg = "";
            bool operacao = false;
           // if (foto.Length > 10 * (1024 * 4))
           // { msg ="Arquivo Muito Grande"; }
           // else
            {
                CamadaAcessoDados.UsuarioBD ubd = new CamadaAcessoDados.UsuarioBD();
                operacao = ubd.IncluirFoto(id,foto);
            
            }
            return (operacao,msg);
        }
    }
}
