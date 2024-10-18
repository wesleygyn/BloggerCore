namespace BloggerCore.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? Nascimento { get; set; }
        public string Email { get; set; }
        public string Telefone{ get; set; }
        public string Cidade { get; set; }
    }
}
