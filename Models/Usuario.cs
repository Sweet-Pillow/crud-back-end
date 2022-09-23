using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crud_back_end.Models{
    [Table("usuario")]
    public class Usuario {
        
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string? Nome { get; set; }

        [Column("token")]
        public string? Token { get; set; }

        [Column("data_cadastro")]
        public DateTime? DataCadastro { get; set; }
        //Banco com _ e letra minuscula
        //No modelo com letra maiuscula, sem _
    }
}