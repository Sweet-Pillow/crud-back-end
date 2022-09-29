using crud_back_end.Repositories.Interfaces;
using crud_back_end.Data;
using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Repositories.Implementations
{
    public class ContatoRepositorie : IContatoRepositorie
    {
        private readonly AppDbContext _context;

        public ContatoRepositorie(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> GetContatosAsync(string token) 
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var listaContatos = _context.contato.ToList();

            return listaContatos;
        }

    }
}