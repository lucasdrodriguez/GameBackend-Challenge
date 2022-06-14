using GameBackend_Challenge.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Darewise_Challenge.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Bug> Bugs { get; set; }

        public DbSet<Player> Players { get; set; }  



    }
}
