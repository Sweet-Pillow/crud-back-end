using crud_back_end.Repositories.Interfaces;
using crud_back_end.Data;
using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Repositories.Implementations
{
    public class UsuarioRepositorie : IUsuarioRepositorie
    {
        private readonly AppDbContext _context;

        public UsuarioRepositorie(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CreateAsync(CreateUsuarioDTO createUsuario)
        {
            var usuario = new Usuario();
            usuario.Nome = createUsuario.nome;
            usuario.Token = Guid.NewGuid().ToString();
            usuario.DataCadastro = DateTime.UtcNow;

            _context.usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}