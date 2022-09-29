using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Repositories.Interfaces
{
    public interface IContatoRepositorie
    {
        public Task<Contato> CreateContatoAsync(CreateContatoDTO createUserDTO, string token);
        public Task<IEnumerable<Contato>> GetContatosAsync(string token);
    }
}