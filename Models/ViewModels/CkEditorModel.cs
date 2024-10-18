using System.ComponentModel.DataAnnotations;

namespace BloggerCore.Models.ViewModels
{
    public class CkEditorModel
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string ConteudoHTML { get; set; }
    }
}
