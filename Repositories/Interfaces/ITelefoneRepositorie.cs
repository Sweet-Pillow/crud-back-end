using crud_back_end.Models;
using crud_back_end.DTOs;

namespace crud_back_end.Repositories.Interfaces
{
    public interface ITelefoneRepositorie
    {
        public Task<HistoricoLigacao> GetChamadaEmAndamentoAsync(string token);
        public Task<IEnumerable<HistoricoLigacao>> GetChamadasAsync(string token);
        public Task<HistoricoLigacao> GetChamadaByIdAsync(string token, int id);
    }
}