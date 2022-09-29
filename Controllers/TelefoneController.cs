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
    public class TelefoneController : ControllerBase {
        private readonly ITelefoneRepositorie _telefoneRepositorie;
        public TelefoneController(ITelefoneRepositorie telefoneRepositorie) => _telefoneRepositorie = telefoneRepositorie;


        [HttpGet]
        public async Task<ActionResult<HistoricoLigacao>> GetChamadas([FromRoute] string token)
        {
            try
            {
                var listaChamada = await _telefoneRepositorie.GetChamadasAsync(token);
                
                if (listaChamada == null)
                {
                    return NotFound();
                }

                return Ok(listaChamada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("contato/{idContato}")]
        public async Task<ActionResult<IEnumerable<HistoricoLigacao>>> GetChamadaByContatoId([FromRoute] string token,[FromRoute] int idContato)
        {
            try
            {
                var listaChamadaContato = await _telefoneRepositorie.GetChamadaByContatoIdAsync(token, idContato);

                if (listaChamadaContato == null)
                {
                    return NotFound();
                }

                return Ok(listaChamadaContato);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // [HttpGet("contato/{idContato}")]
        // public async Task<IActionResult> GetChamadaByIdContato([FromRoute] string token, [FromRoute] int idContato){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).FirstOrDefaultAsync();

        //     if(valid_token == null){
        //         return NotFound();
        //     }

        //     var contato = await _context.contato.FindAsync(idContato);

        //     if(contato == null){
        //         return NotFound("Contato nao encontrado");
        //     }

        //     var chamadasContato = await _context.historico_ligacao.Where(hist => hist.ContatoId == idContato).Include(cont => cont.Contato).ToListAsync();

        //     return Ok(chamadasContato);
        // }

        [HttpGet("chamada-em-andamento")]
        public async Task<ActionResult<HistoricoLigacao>> GetChamadaEmAndamento([FromRoute] string token)
        {
            try
            {
                var chamada = await _telefoneRepositorie.GetChamadaEmAndamentoAsync(token);
                
                if (chamada == null)
                {
                    return NotFound();
                }

                return Ok(chamada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoLigacao>> GetChamadaById([FromRoute] string token, int id)
        {
            try
            {
                var chamada = await _telefoneRepositorie.GetChamadaByIdAsync(token, id);

                if (chamada == null)
                {
                    return NotFound();
                }

                return Ok(chamada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<HistoricoLigacao>> CreateChamada([FromRoute] string token, CreateChamadaDTO createChamada)
        {
            try
            {
                var chamada = await _telefoneRepositorie.CreateChamadaAsync(token, createChamada);

                if (chamada == null)
                {
                    return NotFound(chamada);
                }

                return Ok(chamada);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HistoricoLigacao>> UpdateChamada([FromRoute] string token, int id, UpdateChamadaDTO updateChamada)
        {
            try
            {
                var chamada = await _telefoneRepositorie.UpdateChamadaAsync(token, id, updateChamada);

                if (chamada == null)
                {
                    return null;
                }

                return Ok(chamada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}