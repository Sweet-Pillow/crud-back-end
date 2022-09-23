using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_back_end.Data;
using crud_back_end.Models;

namespace crud_back_end.Controllers{
    [Route("api/Telefone")]
    [ApiController]
    public class TelefoneController : ControllerBase {
        private readonly AppDbContext _context;
        public TelefoneController(AppDbContext context) => _context = context;
        
        [HttpGet]
        public async Task<IEnumerable<Contato>> Get() 
            => await _context.contato.ToListAsync();
    }
}