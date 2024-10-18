using BloggerCore.Models;
using BloggerCore.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggerCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Postagem> postagens { get; set; }
        public DbSet<Pessoa> pessoas { get; set; }

        public DbSet<CkEditorModel> ckEditor { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}