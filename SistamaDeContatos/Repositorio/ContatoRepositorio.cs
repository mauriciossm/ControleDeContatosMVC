using SistamaDeContatos.Data;
using SistamaDeContatos.Models;

namespace SistamaDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public bool Verificao(ContatoModel contato)
        {
            if (contato != null)
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
            ContatoModel contatoDB = ListarPorId(id);
            if (Verificao(contatoDB))
            {
                _bancoContext.Contatos.Remove(contatoDB);
                _bancoContext.SaveChanges();
                return true;
            }

            throw new Exception("Houve um erro na deleção do contato");
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);
            
            if (contatoDB == null)
            {
                throw new Exception("Houve um erro ao atualizar o contato");
            }
            else
            {
                contatoDB.Nome = contato.Nome;
                contatoDB.Email = contato.Email;
                contatoDB.Celular = contato.Celular;

                _bancoContext.Contatos.Update(contatoDB);
                _bancoContext.SaveChanges();
                return contatoDB;
            }
            
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }

        public ContatoModel ListarPorId(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
