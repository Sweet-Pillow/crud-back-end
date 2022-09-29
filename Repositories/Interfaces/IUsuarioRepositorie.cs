using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Repositories.Interfaces
{
    public interface IUsuarioRepositorie
    {
        public Task<Usuario> CreateAsync(CreateUsuarioDTO createUserDTO);
        public Task<IEnumerable<Usuario>> GetUsuariosAsync();
    }
}