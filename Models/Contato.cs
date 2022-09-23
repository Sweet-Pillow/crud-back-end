using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crud_back_end.Models{

    [Table("contato")]
    public class Contato {

        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("nome")]
        public string? Nome { get; set; }
        
        [Column("telefone")]
        public string? Telefone { get; set; }
        
        [Column("email")]
        public string? Email { get; set; }
        
        [Required]
        [Column("ativo")]
        public bool Ativo { get; set; }
        
        [Required]
        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }
        
        [Column("data_cadastro")]
        public DateTime? DataCadastro { get; set; }
        
        [Column("data_edicao")]
        public DateTime? DataEdicao { get; set; }
        
        [Column("id_usuario")]
        public int? UsuarioId { get; set; }        
        public Usuario Usuario { get; set; }
        //Banco com _ e letra minuscula
        //No modelo com letra maiuscula, sem _
    }
}