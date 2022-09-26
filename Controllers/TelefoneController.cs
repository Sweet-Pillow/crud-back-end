using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_back_end.Data;
using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Controllers{
    [Route("api/[controller]/{token}")]
    [ApiController]
    public class TelefoneController : ControllerBase {
        private readonly AppDbContext _context;
        public TelefoneController(AppDbContext context) => _context = context;
        
        // [HttpGet("chamada-em-andamento")]
        // public async Task<IActionResult> 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChamadaById([FromRoute] string token, int id){
            var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

            if(valid_token.Count == 0){
                return NotFound();
            }

            var contato = await _context.contato.FindAsync(id);

            if (contato == null){
                return NotFound();
            }

            var chamada = await _context.historico_ligacao.Where(hist => hist.InicioAtendimento != null).ToListAsync();

            if(chamada == null){
                return NotFound();
            }

            return Ok(chamada);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] string token, CreateChamadaDTO createChamada){
            var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

            if(valid_token.Count == 0){
                return NotFound();
            }

            var contato = await _context.contato.FindAsync(createChamada.idContato);

            if(contato == null){
                return NotFound();
            }

            var ocorrendoChamada = await _context.historico_ligacao.Where(hist => hist.ContatoId == contato.Id).ToListAsync();

            if (ocorrendoChamada != null){
                return BadRequest("Você já está em ligação com " + contato.Nome);
            }

            var chamada = new HistoricoLigacao();

            chamada.ContatoId = createChamada.idContato;
            chamada.InicioAtendimento = DateTime.UtcNow;

            await _context.historico_ligacao.AddAsync(chamada);
            await _context.SaveChangesAsync();

            return Ok(chamada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string token, int id, UpdateChamadaDTO updateChamada){
            var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

            if(valid_token.Count == 0){
                return NotFound();
            }

            var chamada = await _context.historico_ligacao.FindAsync(id);

            if(chamada == null){
                return NotFound();
            }
            
            chamada.FimAtendimento = DateTime.UtcNow;
            chamada.Assunto = updateChamada.Assunto;
            
            return Ok(chamada);
        }
    }
}