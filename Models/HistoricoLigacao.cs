using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crud_back_end.Models{
    [Table("historico_ligacao")]
    public class HistoricoLigacao{
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_contato")]
        public int ContatoId { get; set; }
        public Contato Contato { get; set; }

        [Column("inicio_atendimento")]
        public DateTime InicioAtendimento { get; set; }
        
        [Column("fim_atendimento")]
        public DateTime? FimAtendimento { get; set; }
        
        [Column("assunto")]
        public string? Assunto { get; set; }
    }
}