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
    [Route("/api/participants")]
    public class BarbequeParticipantsController : Controller
    {
        private readonly IBarbequeParticipantService _bbqParticipantsService;

        public BarbequeParticipantsController (IBarbequeParticipantService bbqParticipantsService)
        {
            _bbqParticipantsService = bbqParticipantsService;
        }

        /// <summary>
        /// Busca todos os participantes de um churras específico
        /// </summary>
        /// <returns>Lista de participantes</returns>
        /// <response code="200">Retorna o array dos participantes</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("barbeque/{barbequeId:int}")]
        [HttpGet]
        public async Task<IEnumerable<BarbequeParticipant>> GetAllByBarbequeAsync(int barbequeId)
        {
            var participants = await _bbqParticipantsService.ListByBarbequeAsync(barbequeId);
            return participants;
        }

        /// <summary>
        /// Busca um participante específico
        /// </summary>
        /// <returns>Model de participante</returns>
        /// <response code="200">Retorna o model do participante solicitado</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("{id:int}")]
        [HttpGet]
        public async Task<BarbequeParticipant> GetOneAsync(int id)
        {
            var participant = await _bbqParticipantsService.FindByIdAsync(id);
            return participant;
        }

        /// <summary>
        /// Insere um participante específico
        /// </summary>
        /// <returns>Model de participante</returns>
        /// <response code="200">Retorna o model do participante inserido</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("")]
        [HttpPost]
        public async Task<ActionResult> InsertAsync ([FromBody]AddBarbequeParticipantModel data)
        {
            try {
                if (string.IsNullOrEmpty(data.Name))
                    throw new ArgumentNullException(nameof(data.Name));
                
                if (!(data.Contribution >= 0))
                    throw new ArgumentException(nameof(data.Contribution));

                if (!(data.BarbequeId >= 1))
                    throw new ArgumentException(nameof(data.BarbequeId));

                return Json(await _bbqParticipantsService.AddAsync(data));
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
        /// Atualiza um participante específico
        /// </summary>
        /// <returns>Model de participante</returns>
        /// <response code="200">Retorna o model do participante atualizado</response>
        /// <response code="400">Parâmetros enviados estão incorretos</response>
        /// <response code="422">Processamento encerrado, analise a mensagem retornada</response>
        [Route("{id:int}")]
        [HttpPut]
        public async Task<ActionResult> UpdateAsync (int id, [FromBody]AddBarbequeParticipantModel data)
        {
            try 
            {
                if (string.IsNullOrEmpty(data.Name))
                    throw new ArgumentNullException(nameof(data.Name));
                
                if (!(data.Contribution >= 0))
                    throw new ArgumentException(nameof(data.Contribution));

                if (!(data.BarbequeId >= 1))
                    throw new ArgumentException(nameof(data.BarbequeId));

                return Json(await _bbqParticipantsService.UpdateAsync(id, data));
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
        /// Remove um participante específico
        /// </summary>
        /// <returns>Mensagem com o sucesso da operação</returns>
        /// <response code="200">Retorna uma mensagem informando o sucesso da operação</response>
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

                bool success = await _bbqParticipantsService.DeleteAsync(id);

                if (success)
                    return Ok("Removido com sucesso");

                throw new Exception("Não conseguimos remover este participante");
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