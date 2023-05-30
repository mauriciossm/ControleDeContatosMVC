using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Helper;
using SistamaDeContatos.Models;
using SistamaDeContatos.Repositorio;

namespace SistamaDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {

            // Se o usuário estiver logado, redirecionar para home
            if (_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }
                return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                

                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (_usuarioRepositorio.Verificao(usuario))
                    {
                        if (usuario.VerificarSenha(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");

                        }
                        TempData["MensagemErro"] = "Senha inválida, tente novamente.";
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Login ou/e senha inválido(s), tente novamente.";

                    }

                }
                return View(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível realizar o login. Detalhes do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
