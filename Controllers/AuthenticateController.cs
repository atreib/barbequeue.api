using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using barbequeue.api.Domain.UseCases;

namespace barbequeue.api.Controllers
{
    [Route("/api/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService _authService;

        public AuthenticateController (IAuthenticateService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autenticação por usuário e senha
        /// </summary>
        /// <returns>Objeto do usuário com a JWT para utilização</returns>
        /// <response code="200">Retorna o objeto do usuário com a JWT para utilização</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("")]
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody]LoginModel loginData)
        {
            try {
                if (string.IsNullOrEmpty(loginData.Username))
                    throw new ArgumentNullException(nameof(loginData.Username));
                
                if (string.IsNullOrEmpty(loginData.Password))
                    throw new ArgumentNullException(nameof(loginData.Password));

                return Json(await _authService.Authenticate(loginData));
            } 
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is ArgumentException)
                    return BadRequest(ex);
                
                if (ex is ApplicationException)
                    return UnprocessableEntity(ex);

                return StatusCode(500, new Exception("Ops, tivemos um problema"));
            }
        }
    }
}