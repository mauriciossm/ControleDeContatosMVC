using SistamaDeContatos.Models;

namespace SistamaDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel ListarPorId(int id);
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email,string login);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha);

        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Apagar(int id);

        bool Verificao(UsuarioModel usuario);

    }
}
