using crud_back_end.Repositories.Interfaces;
using crud_back_end.Data;
using crud_back_end.Models;
using crud_back_end.DTOs;
using Microsoft.EntityFrameworkCore;

namespace crud_back_end.Repositories.Implementations
{
    public class TelefoneRepositorie : ITelefoneRepositorie
    {
        private readonly AppDbContext _context;

        public TelefoneRepositorie(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HistoricoLigacao> GetChamadaEmAndamentoAsync(string token)
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var verificarChamada = await _context.historico_ligacao.Where(hist => hist.InicioAtendimento != null && hist.FimAtendimento == null).Include(h => h.Contato).FirstOrDefaultAsync();

            if(verificarChamada == null){
                return null;
            }

            return verificarChamada;
        }

        public async Task<IEnumerable<HistoricoLigacao>> GetChamadasAsync(string token)
        {   
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefaultAsync();

            if (valid_token == null)
            {
                return null;
            }

            var listaChamadas = await _context.historico_ligacao.Include(h => h.Contato).ToListAsync();

            return listaChamadas;
        }

        public async Task<HistoricoLigacao> GetChamadaByIdAsync(string token, int id)
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var chamada = await _context.historico_ligacao.Where(hist => hist.Id == id).FirstOrDefaultAsync();
            
            if(chamada == null){
                return null;
            }

            return chamada;
        }
    }
}