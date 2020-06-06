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

        [Route("barbeque/{barbequeId:int}")]
        [HttpGet]
        public async Task<IEnumerable<BarbequeParticipant>> GetAllByBarbequeAsync(int barbequeId)
        {
            var participants = await _bbqParticipantsService.ListByBarbequeAsync(barbequeId);
            return participants;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<BarbequeParticipant> GetOneAsync(int id)
        {
            var participant = await _bbqParticipantsService.FindByIdAsync(id);
            return participant;
        }

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