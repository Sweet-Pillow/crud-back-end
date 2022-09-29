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

            var listaChamadas = _context.historico_ligacao.Include(h => h.Contato).ToList();

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

        public async Task<HistoricoLigacao> CreateChamadaAsync(string token, CreateChamadaDTO createChamada)
        {
            var valid_token = _context.usuario.Where(u => u.Token == token).FirstOrDefault();

            if (valid_token == null)
            {
                return null;
            }

            var contato = await _context.contato.FindAsync(createChamada.idContato);

            if(contato == null){
                return null;
            }

            var ocorrendoChamada = await _context.historico_ligacao.Where(hist => hist.InicioAtendimento != null && hist.FimAtendimento == null).FirstOrDefaultAsync();

            if (ocorrendoChamada != null){
                return null;
            }

            var chamada = new HistoricoLigacao();

            chamada.ContatoId = createChamada.idContato;
            chamada.InicioAtendimento = DateTime.UtcNow;

            await _context.historico_ligacao.AddAsync(chamada);
            await _context.SaveChangesAsync();

            chamada.Contato = contato;

            return chamada;
        }

        public async Task<HistoricoLigacao> UpdateChamadaAsync(string token, int id, UpdateChamadaDTO updateChamada)
        {
            var valid_token = await _context.usuario.Where(user => user.Token == token).FirstOrDefaultAsync();

            if(valid_token == null){
                return null;
            }

            var chamada = await _context.historico_ligacao.FindAsync(id);

            if(chamada == null){
                return null;
            }
            
            var chamadaEmAndamento = await _context.historico_ligacao.Where(hist => (hist.InicioAtendimento != null && hist.FimAtendimento == null)).FirstOrDefaultAsync();

            if(chamadaEmAndamento == null){
                return null;
            }

            if(chamadaEmAndamento.Id != chamada.Id){
                return null;
            }

            chamada.FimAtendimento = DateTime.UtcNow;
            chamada.Assunto = updateChamada.Assunto;

            await _context.SaveChangesAsync();
            
            return chamada;
        }
    }
}