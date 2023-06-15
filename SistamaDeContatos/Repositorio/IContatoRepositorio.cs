using SistamaDeContatos.Models;

namespace SistamaDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel Adicionar(ContatoModel contato);

        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);

        bool Verificao(ContatoModel contato);

    }
}
