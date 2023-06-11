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
        private readonly IEmail _email;


        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
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

        public IActionResult RedefinirSenha()
        {
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

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirsenhaModel redefinirsenhaModel)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirsenhaModel.Email, redefinirsenhaModel.Login);

                    if (_usuarioRepositorio.Verificao(usuario))
                    {
                        string novaSenha = usuario.GerarNovaSenha();

                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = "Enviamos para seu e-mail cadastrado uma nova senha";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não conseguimos enviar o e-mail com uma nova senha. Por favor, tente novamente";

                        }

                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = "Não foi possível redefinir sua senha. Verifique os dados informados.";



                }
                return View(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível redefinir sua senha. Detalhes do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
