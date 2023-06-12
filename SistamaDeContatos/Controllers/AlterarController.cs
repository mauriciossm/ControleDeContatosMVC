using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Helper;
using SistamaDeContatos.Models;
using SistamaDeContatos.Repositorio;

namespace SistamaDeContatos.Controllers
{
    public class AlterarController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public AlterarController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenha)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                alterarSenha.Id = usuarioLogado.Id;
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(alterarSenha);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return View(nameof(Index), alterarSenha);
                }
                return View(nameof(Index), alterarSenha);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível alterar a senha. Detalhe do erro: {erro.Message}";

                return View(nameof(Index), alterarSenha);
            }
        }
    }
}
