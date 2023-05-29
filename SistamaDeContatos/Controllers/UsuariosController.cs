using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Models;
using SistamaDeContatos.Repositorio;

namespace SistamaDeContatos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
                if (_usuarioRepositorio.Verificao(usuario)) 
                {
                    return View(usuario);
                }
                else
                {
                    return RedirectToAction("PaginaErro", "Home");
                }
            
        }
        public IActionResult ConfirmacaoApagar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            if (_usuarioRepositorio.Verificao(usuario))
            {
                return View(usuario);
            }
            else
            {
                return RedirectToAction("PaginaErro", "Home");
            }
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "usuário apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível apagar o usuário";
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception erro) 
            {
                TempData["MensagemErro"] = $"Não foi possível apagar o usuário. Detalhes do erro: {erro.Message}";
                return RedirectToAction("PaginaErro", "Home");

            }
        }
        
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    _usuarioRepositorio.Adicionar(usuario);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(usuario);
                }
            }
            catch (Exception erro) 
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar o usuário. Detalhes do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {

            try
            {
                UsuarioModel usuario = null;

                usuario = new UsuarioModel()
                {
                    Id = usuarioSemSenhaModel.Id,
                    Nome = usuarioSemSenhaModel.Nome,
                    Email = usuarioSemSenhaModel.Email,
                    Login = usuarioSemSenhaModel.Login,
                    Perfil = usuarioSemSenhaModel.Perfil
                };
                if (ModelState.IsValid)
                {
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso";
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(nameof(Editar), usuario);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível alterar o usuário. Detalhe do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
