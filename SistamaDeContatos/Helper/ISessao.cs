using SistamaDeContatos.Models;

namespace SistamaDeContatos.Helper
{
    public interface ISessao
    {        
        void CriarSessaoUsuario(UsuarioModel usuario);
        void RemoverSessaoUsuario();
        UsuarioModel BuscarSessaoUsuario();
    }
}
