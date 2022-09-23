namespace crud_back_end.DTOs{
    public class CreateContatoDTO {
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}