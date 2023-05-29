using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Models;
using SistamaDeContatos.Repositorio;

namespace SistamaDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
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
                            return RedirectToAction("Index", "Home");

                        }
                        TempData["MensagemErro"] = "Senha inválida, tente novamente.";
                        return RedirectToAction("Index");


                    }
                    TempData["MensagemErro"] = "Login ou/e senha inválido(s), tente novamente.";

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
