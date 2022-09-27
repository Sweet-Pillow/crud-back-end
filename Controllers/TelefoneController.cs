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

        [HttpGet]
        public async Task<IActionResult> GetChamadas([FromRoute] string token){
            var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

            if(valid_token.Count == 0){
                return NotFound();
            }

            var listaChamadas = await _context.historico_ligacao.Include(h => h.Contato).ToListAsync();

            return Ok(listaChamadas);
        }

        [HttpGet("chamada-em-andamento")]
        public async Task<IActionResult> GetChamadaEmAndamento([FromRoute] string token){
            var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

            if(valid_token.Count == 0){
                return NotFound();
            }

            var VerificarChamada = await _context.historico_ligacao.Where(hist => hist.InicioAtendimento != null && hist.FimAtendimento == null).ToListAsync();

            if(VerificarChamada.Count == 0){
                return NotFound();
            }

            return Ok(VerificarChamada);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChamadaById([FromRoute] string token, int id){
            var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

            if(valid_token.Count == 0){
                return NotFound();
            }

            var chamada = await _context.historico_ligacao.Where(hist => hist.Id == id).ToListAsync();
            
            if(chamada.Count == 0){
                return NotFound("Ligação não localizada ");
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
                return NotFound("Contato não encontrado");
            }

            var ocorrendoChamada = await _context.historico_ligacao.Where(hist => hist.InicioAtendimento != null && hist.FimAtendimento == null).ToListAsync();

            if (ocorrendoChamada.Count != 0){
                return BadRequest("Você já está em ligação com " );
            }

            var chamada = new HistoricoLigacao();

            chamada.ContatoId = createChamada.idContato;
            chamada.InicioAtendimento = DateTime.UtcNow;

            await _context.historico_ligacao.AddAsync(chamada);
            await _context.SaveChangesAsync();
            chamada.Contato = contato;

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
                return NotFound("Chamada não encontrada");
            }
            
            var chamadaEmAndamento = await _context.historico_ligacao.Where(hist => (hist.InicioAtendimento != null && hist.FimAtendimento == null)).FirstOrDefaultAsync();

            if(chamadaEmAndamento == null){
                return NotFound();
            }

            if(chamadaEmAndamento.Id != chamada.Id){
                return NotFound("Contato não possui chamada em andamento");
            }

            chamada.FimAtendimento = DateTime.UtcNow;
            chamada.Assunto = updateChamada.Assunto;

            await _context.SaveChangesAsync();
            
            return Ok(chamada);
        }
    }
}