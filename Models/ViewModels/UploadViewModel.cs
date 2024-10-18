namespace BloggerCore.Models.ViewModels
{
    public class UploadViewModel
    {
        public string Editor { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
