using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Models;
using SistamaDeContatos.Repositorio;

namespace SistamaDeContatos.Controllers
{
    public class ContatosController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatosController(IContatoRepositorio contatoRepositorio) 
        { 
            _contatoRepositorio = contatoRepositorio;
        }
        
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
                if (_contatoRepositorio.Verificao(contato)) 
                {
                    return View(contato);
                }
                else
                {
                    return RedirectToAction(nameof(PaginaErro));
                }
            
        }
        public IActionResult ConfirmacaoApagar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            if (_contatoRepositorio.Verificao(contato))
            {
                return View(contato);
            }
            else
            {
                return RedirectToAction(nameof(PaginaErro));
            }
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contado apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível apagar o contato";
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception erro) 
            {
                TempData["MensagemErro"] = $"Não foi possível apagar o contato. Detalhes do erro: {erro.Message}";
                return RedirectToAction(nameof(PaginaErro));

            }
        }
        
        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    _contatoRepositorio.Adicionar(contato);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(contato);
                }
            }
            catch (Exception erro) 
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar o contato. Detalhes do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    _contatoRepositorio.Atualizar(contato);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(nameof(Editar), contato);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível alterar o contato. Detalhe do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }

        }
        
        public IActionResult PaginaErro()
        {
            return View();
        }

    }
}
