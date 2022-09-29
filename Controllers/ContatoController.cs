using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_back_end.Data;
using crud_back_end.Models;
using crud_back_end.DTOs;
using crud_back_end.Repositories.Interfaces;

namespace crud_back_end.Controllers{
    [Route("api/[controller]/{token}")]
    [ApiController]

    public class ContatoController : ControllerBase {
        private readonly IContatoRepositorie _contatoRepositorie;
        public ContatoController(IContatoRepositorie contatoRepositorie) => _contatoRepositorie = contatoRepositorie;

        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContatoById([FromRoute] string token, [FromRoute] int id)
        {
            try
            {
                var contato = await _contatoRepositorie.GetContatoByIdAsync(token, id);

                if (contato == null)
                {
                    return NotFound();
                }

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContato([FromRoute] string token, [FromRoute] int id)
        {
            try
            {
                var contato = await _contatoRepositorie.DeleteContatoAsync(token, id);

                if (contato == null)
                {
                    return NotFound();
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contato>> UpdateContato([FromRoute] string token, int id, UpdateContatoDTO updateContato)
        {
            try
            {
                var contato = await _contatoRepositorie.UpdateContatoAsync(token, id, updateContato);

                if (contato == null)
                {
                    return NotFound();
                }

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Contato>> GetContatos([FromRoute] string token)
        {
            try 
            {
                var listaContatos = await _contatoRepositorie.GetContatosAsync(token);

                if(listaContatos == null)
                {
                    return NotFound();
                }
                
                return Ok(listaContatos);
            }
            catch (Exception ex )
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Contato>> CreateContato([FromRoute] string token, CreateContatoDTO createContato){
            try
            {
                var contato = await _contatoRepositorie.CreateContatoAsync(createContato, token);
                
                if (contato == null)
                {
                    return NotFound();
                }

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}