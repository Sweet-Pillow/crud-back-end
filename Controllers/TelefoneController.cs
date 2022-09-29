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
        // [HttpGet]
        // public async Task<IActionResult> GetChamadas([FromRoute] string token){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).FirstOrDefaultAsync();

        //     if(valid_token == null){
        //         return NotFound();
        //     }

        //     var listaChamadas = await _context.historico_ligacao.Include(h => h.Contato).ToListAsync();

        //     return Ok(listaChamadas);
        // }

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
        // [HttpGet("chamada-em-andamento")]
        // public async Task<IActionResult> GetChamadaEmAndamento([FromRoute] string token){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

        //     if(valid_token.Count == 0){
        //         return NotFound();
        //     }

        //     var VerificarChamada = await _context.historico_ligacao.Where(hist => hist.InicioAtendimento != null && hist.FimAtendimento == null).Include(h => h.Contato).ToListAsync();

        //     if(VerificarChamada.Count == 0){
        //         return NotFound();
        //     }

        //     return Ok(VerificarChamada);
        // }


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
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetChamadaById([FromRoute] string token, int id){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

        //     if(valid_token.Count == 0){
        //         return NotFound();
        //     }

        //     var chamada = await _context.historico_ligacao.Where(hist => hist.Id == id).ToListAsync();
            
        //     if(chamada.Count == 0){
        //         return NotFound("Ligação não localizada ");
        //     }

        //     return Ok(chamada);
        // }


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
        // [HttpPost]
        // public async Task<IActionResult> Create([FromRoute] string token, CreateChamadaDTO createChamada){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

        //     if(valid_token.Count == 0){
        //         return NotFound();
        //     }

        //     var contato = await _context.contato.FindAsync(createChamada.idContato);

        //     if(contato == null){
        //         return NotFound("Contato não encontrado");
        //     }

        //     var ocorrendoChamada = await _context.historico_ligacao.Where(hist => hist.InicioAtendimento != null && hist.FimAtendimento == null).FirstOrDefaultAsync();

        //     if (ocorrendoChamada != null){
        //         return BadRequest("Você já está em ligação");
        //     }

        //     var chamada = new HistoricoLigacao();

        //     chamada.ContatoId = createChamada.idContato;
        //     chamada.InicioAtendimento = DateTime.UtcNow;

        //     await _context.historico_ligacao.AddAsync(chamada);
        //     await _context.SaveChangesAsync();
        //     chamada.Contato = contato;

        //     return Ok(chamada);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update([FromRoute] string token, int id, UpdateChamadaDTO updateChamada){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).FirstOrDefaultAsync();

        //     if(valid_token == null){
        //         return NotFound();
        //     }

        //     var chamada = await _context.historico_ligacao.FindAsync(id);

        //     if(chamada == null){
        //         return NotFound();
        //     }
            
        //     var chamadaEmAndamento = await _context.historico_ligacao.Where(hist => (hist.InicioAtendimento != null && hist.FimAtendimento == null)).FirstOrDefaultAsync();

        //     if(chamadaEmAndamento == null){
        //         return NotFound();
        //     }

        //     if(chamadaEmAndamento.Id != chamada.Id){
        //         return NotFound("Contato não possui chamada em andamento");
        //     }

        //     chamada.FimAtendimento = DateTime.UtcNow;
        //     chamada.Assunto = updateChamada.Assunto;

        //     await _context.SaveChangesAsync();
            
        //     return Ok(chamada);
        // }
    }
}