using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_back_end.Data;
using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        private readonly AppDbContext _context;
        public UsuarioController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get() 
            => await _context.usuario.ToListAsync();
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsuarioById(int id){
            var usuario = await _context.usuario.FindAsync(id);
            return usuario == null? NotFound() : Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUsuarioDTO createUsuario){
            var usuario = new Usuario();

            usuario.Nome = createUsuario.nome;
            usuario.Token = Guid.NewGuid().ToString();
            usuario.DataCadastro = DateTime.UtcNow;

            await _context.usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioById), new {id = usuario.Id}, usuario);
        }
    }
}