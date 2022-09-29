using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Repositories.Interfaces
{
    public interface IContatoRepositorie
    {
        public Task<Contato> GetContatoByIdAsync(string token, int id);
        public Task<IEnumerable<Contato>> GetContatosAsync(string token);
        public Task<Contato> CreateContatoAsync(CreateContatoDTO createUserDTO, string token);
    }
}