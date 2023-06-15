using Microsoft.AspNetCore.Mvc;
using SistamaDeContatos.Helper;
using SistamaDeContatos.Models;
using SistamaDeContatos.Repositorio;

namespace SistamaDeContatos.Controllers
{
    public class UsuarioPrimeiroRegistroController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public UsuarioPrimeiroRegistroController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario, LoginModel loginModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    _usuarioRepositorio.Adicionar(usuario);
                    usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (_usuarioRepositorio.Verificao(usuario))
                    {
                        if (usuario.VerificarSenha(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");

                        }
                        TempData["MensagemErro"] = "Senha inválida, tente novamente.";
                    }
                    
                        TempData["MensagemErro"] = "Login ou/e senha inválido(s), tente novamente.";
                        return RedirectToAction("LoginController", "Index");                  
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

    }
}
