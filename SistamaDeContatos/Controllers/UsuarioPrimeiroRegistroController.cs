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
                    
                    _usuarioRepositorio.Adicionar(usuario);
                    usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
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
