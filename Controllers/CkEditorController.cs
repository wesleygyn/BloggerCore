using BloggerCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using BloggerCore.Data;

namespace BloggerCore.Controllers
{
    public class CkEditorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CkEditorController(ApplicationDbContext context, IConfiguration configuration, IWebHostEnvironment environment, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _environment = environment;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CkEditorModel modelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modelo);
        }

        public async Task<JsonResult> UploadImagem([FromForm] IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            //your custom code logic here

            //1)check if the file is image

            //2)check if the file is too large

            //etc

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            //save file under wwwroot/CKEditorImages folder

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/imagens",
                fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await upload.CopyToAsync(stream);
            }

            var url = $"{"/imagens/"}{fileName}";


            var imageUrl = "/imagens/" + fileName; // O caminho para a imagem no servidor

            return Json(new { url = imageUrl });

            //var success = new uploadsuccess
            //{
            //    Uploaded = 1,
            //    FileName = fileName,
            //    Url = url
            //};

            //return new JsonResult(success);
        }




        //[HttpPost]
        //public IActionResult UploadImagem(IFormFile image)
        //public async Task<JsonResult> UploadImagem([FromForm] IFormFile image)
        //{
        //    if (image != null && image.Length > 0)
        //    {
        //        // Lógica para salvar a imagem e obter o URL
        //        var nomeArquivo = Guid.NewGuid() + Path.GetExtension(image.FileName);
        //        var caminhoParaSalvar = Path.Combine(_webHostEnvironment.WebRootPath, "imagens", nomeArquivo);

        //        using (var stream = new FileStream(caminhoParaSalvar, FileMode.Create))
        //        {
        //            image.CopyTo(stream);
        //        }

        //        var imageUrl = "/imagens/" + nomeArquivo; // O caminho para a imagem no servidor

        //        return Json(new { url = imageUrl });
        //    }
        //    return Json(new { url = "" });
        //    //return BadRequest("Erro ao fazer o upload da imagem.");

        //}


    }








    //[HttpPost]
    //public IActionResult UploadImagem(IFormFile image)
    //{
    //    if (image != null && image.Length > 0)
    //    {
    //        // Lógica para salvar a imagem e obter o URL
    //        var nomeArquivo = Guid.NewGuid() + Path.GetExtension(image.FileName);
    //        var caminhoParaSalvar = Path.Combine(_webHostEnvironment.WebRootPath, "imagens", nomeArquivo);

    //        using (var stream = new FileStream(caminhoParaSalvar, FileMode.Create))
    //        {
    //            image.CopyTo(stream);
    //        }

    //        var imageUrl = "/imagens/" + nomeArquivo; // O caminho para a imagem no servidor

    //        return Json(new { url = imageUrl });
    //    }

    //    return BadRequest("Erro ao fazer o upload da imagem.");
    //}
































    //[HttpGet]
    //public IActionResult Index()
    //{
    //    var model = new UploadViewModel();
    //    return View(model);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Index(UploadViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        string imageUrl = null;
    //        string content = model.Editor;

    //        if (model.ImageUpload != null && model.ImageUpload.Length > 0)
    //        {
    //            imageUrl = await SaveImage(model.ImageUpload);
    //        }

    //        await SaveData(content, imageUrl);

    //        return RedirectToAction("Index");
    //    }

    //    return View(model);
    //}

    //private async Task<string> SaveImage(IFormFile imageUpload)
    //{
    //    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageUpload.FileName)}";
    //    string imagePath = Path.Combine("images", fileName);
    //    string imagePhysicalPath = Path.Combine(_environment.WebRootPath, imagePath);

    //    using (var stream = new FileStream(imagePhysicalPath, FileMode.Create))
    //    {
    //        await imageUpload.CopyToAsync(stream);
    //    }

    //    return imagePath;
    //}

    //private async Task SaveData(string content, string imageUrl)
    //{
    //    using (var connection = new MySqlConnection(_connectionString))
    //    {
    //        await connection.OpenAsync();

    //        var query = "INSERT INTO posts (Content, ImageUrl) VALUES (@Content, @ImageUrl)";

    //        using (var command = new MySqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@Content", content);
    //            command.Parameters.AddWithValue("@ImageUrl", imageUrl);

    //            await command.ExecuteNonQueryAsync();
    //        }
    //    }
    //}
}

