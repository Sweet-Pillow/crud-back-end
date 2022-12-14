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

        public async Task<Contato> GetContatoByIdAsync(string token, int id)
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var contato = await _context.contato.FindAsync(id);

            if (contato == null)
            {
                return null;
            }
            return contato;

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

        public async Task<Contato> CreateContatoAsync(CreateContatoDTO createContato, string token)
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var contato = new Contato();
            contato.Nome = createContato.Nome;
            contato.Telefone = createContato.Telefone;
            contato.Email = createContato.Email;
            contato.Ativo = createContato.Ativo;
            contato.DataNascimento = createContato.DataNascimento;
            contato.DataCadastro = DateTime.UtcNow;
            contato.DataEdicao = null;
            contato.UsuarioId = valid_token.Id;

            await _context.contato.AddAsync(contato);
            await _context.SaveChangesAsync();

            return contato;
        }

        public async Task<Contato> UpdateContatoAsync(string token, int id, UpdateContatoDTO updateContato)
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var contato = await _context.contato.FindAsync(id);

            if (contato == null){
                return null;
            }

            contato.Nome = updateContato.Nome;
            contato.Telefone = updateContato.Telefone;
            contato.Email = updateContato.Email;
            contato.Ativo = updateContato.Ativo;
            contato.DataNascimento = updateContato.DataNascimento;
            contato.DataEdicao = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return contato;
        }

        public async Task<Contato> DeleteContatoAsync(string token, int id)
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var contato = await _context.contato.FindAsync(id);

            if (contato == null){
                return null;
            }

            _context.contato.Remove(contato);
            await _context.SaveChangesAsync();

            return contato;
        }
    }
}