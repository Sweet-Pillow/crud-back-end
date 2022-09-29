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

        
        // [HttpGet]
        // public async Task<IEnumerable<Contato>> Get([FromRoute] string token) => await _context.contato.ToListAsync();
        
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetContatoById([FromRoute] string token, [FromRoute] int id){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

        //     if(valid_token.Count == 0){
        //         return NotFound();
        //     }

        //     var contato = await _context.contato.FindAsync(id);

        //     return contato == null? NotFound(): Ok(contato);
        // }
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

        // [HttpDelete("{id}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> Delete([FromRoute] string token, int id){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

        //     if(valid_token.Count == 0){
        //         return NotFound();
        //     }

        //     var deletarContato = await _context.contato.FindAsync(id);

        //     if(deletarContato == null){
        //         return NotFound();
        //     }

        //     _context.contato.Remove(deletarContato);
        //     await _context.SaveChangesAsync();
            
        //     return NoContent();
        // }
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

        // [HttpPut("{id}")]

        // public async Task<IActionResult> Update([FromRoute] string token, int id, UpdateContatoDTO updateContato){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

        //     if(valid_token.Count == 0){
        //         return NotFound();
        //     }

        //     var contato = await _context.contato.FindAsync(id);

        //     if (contato == null){
        //         return NotFound();
        //     }

        //     contato.Nome = updateContato.Nome;
        //     contato.Telefone = updateContato.Telefone;
        //     contato.Email = updateContato.Email;
        //     contato.Ativo = updateContato.Ativo;
        //     contato.DataNascimento = updateContato.DataNascimento;
        //     contato.DataEdicao = DateTime.UtcNow;

        //     await _context.SaveChangesAsync();

        //     return Ok(contato);
        // }

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

        // [HttpGet]
        // [ProducesResponseType(typeof(Contato), StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> GetContato([FromRoute] string token){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).FirstOrDefaultAsync();

        //     if(valid_token == null){
        //         return NotFound();
        //     }

        //     var contatos = await _context.contato.Where(cont => cont.UsuarioId == valid_token.Id).ToListAsync();

        //     return Ok(contatos);
        // }

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

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status201Created)]
        // public async Task<IActionResult> Create([FromRoute] string token, CreateContatoDTO createContato){
        //     var valid_token = await _context.usuario.Where(user => user.Token == token).ToListAsync();

        //     if(valid_token.Count == 0){
        //         return NotFound();
        //     }

        //     var contato = new Contato();
        //     contato.Nome = createContato.Nome;
        //     contato.Telefone = createContato.Telefone;
        //     contato.Email = createContato.Email;
        //     contato.Ativo = createContato.Ativo;
        //     contato.DataNascimento = createContato.DataNascimento;
        //     contato.DataCadastro = DateTime.UtcNow;
        //     contato.DataEdicao = null;
        //     contato.UsuarioId = valid_token[0].Id;

        //     await _context.contato.AddAsync(contato);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(GetContatoById), "Contato", new {token=token, id=contato.Id} , contato);
        // }
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