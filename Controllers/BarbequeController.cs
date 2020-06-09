using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using barbequeue.api.Domain.UseCases;
using barbequeue.api.Domain.Models;

namespace barbequeue.api.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    public class BarbequeController : Controller
    {
        private readonly IBarbequeService _barbequeService;

        public BarbequeController (IBarbequeService barbequeService)
        {
            _barbequeService = barbequeService;
        }

        /// <summary>
        /// Busca todos os churras que ainda não aconteceram (Data >= Hoje)
        /// </summary>
        /// <returns>Lista de churras</returns>
        /// <response code="200">Retorna o array dos churras</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Barbeque>> GetAllAsync()
        {
            var barbeques = await _barbequeService.ListAsync();
            return barbeques;
        }

        /// <summary>
        /// Busca um churras específico
        /// </summary>
        /// <returns>Model do churras solictado</returns>
        /// <response code="200">Retorna o churras</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("{id:int}")]
        [HttpGet]
        public async Task<Barbeque> GetOneAsync(int id)
        {
            var barbeque = await _barbequeService.FindByIdAsync(id);
            return barbeque;
        }

        /// <summary>
        /// Insere um churras
        /// </summary>
        /// <returns>Model do churras inserido</returns>
        /// <response code="200">Retorna o churras</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("")]
        [HttpPost]
        public async Task<ActionResult> InsertAsync ([FromBody]AddBarbequeModel model)
        {
            try {
                if (string.IsNullOrEmpty(model.Description))
                    throw new ArgumentNullException(nameof(model.Description));
                
                if (model.EventDateTime == null)
                    throw new ArgumentNullException(nameof(model.EventDateTime));

                return Json(await _barbequeService.AddAsync(model));
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

        /// <summary>
        /// Atualiza um churras específico
        /// </summary>
        /// <returns>Model do churras atualizado</returns>
        /// <response code="200">Retorna o churras</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("{id:int}")]
        [HttpPut]
        public async Task<ActionResult> UpdateAsync (int id, [FromBody]AddBarbequeModel data)
        {
            try 
            {
                if (data.EventDateTime == null)
                    throw new ArgumentNullException(nameof(data.EventDateTime));

                if (!(id >= 1))
                    throw new ArgumentException(nameof(id));

                return Json(await _barbequeService.UpdateAsync(id, data));
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

        /// <summary>
        /// Remove um churras específico
        /// </summary>
        /// <returns>Mensagem informando o sucesso da operação</returns>
        /// <response code="200">Texto simples informando o sucesso</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync (int id)
        {
            try 
            {
                if (!(id >= 1))
                    throw new ArgumentException("ID inválido");

                bool success = await _barbequeService.DeleteAsync(id);

                if (success)
                    return Ok("Removido com sucesso");

                throw new ApplicationException("Não conseguimos remover este churrasco");
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