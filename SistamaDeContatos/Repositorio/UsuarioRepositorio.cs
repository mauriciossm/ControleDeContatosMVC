using SistamaDeContatos.Data;
using SistamaDeContatos.Models;

namespace SistamaDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login == login);
#pragma warning restore CS8603 // Possible null reference return.
        }
        public bool Verificao(UsuarioModel usuario)
        {
            if (usuario != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);
            if (Verificao(usuarioDB))
            {
                _bancoContext.Usuarios.Remove(usuarioDB);
                _bancoContext.SaveChanges();
                return true;
            }

            throw new Exception("Houve um erro na deleção do usuario");
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);
            
            if (usuarioDB == null)
            {
                throw new Exception("Houve um erro ao atualizar o usuário");
            }
            else
            {
                usuarioDB.Nome = usuario.Nome;
                usuarioDB.Email = usuario.Email;
                usuarioDB.Login = usuario.Login;
                usuarioDB.Perfil = usuario.Perfil;
                usuarioDB.DataAtualizacao = DateTime.Now;

                _bancoContext.Usuarios.Update(usuarioDB);
                _bancoContext.SaveChanges();
                return usuarioDB;
            }
            
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel ListarPorId(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
