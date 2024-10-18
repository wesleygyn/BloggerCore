namespace BloggerCore.Models
{
    public class Postagem
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Autor { get; set; }
        public string Operacao { get; set; }
    }
}
