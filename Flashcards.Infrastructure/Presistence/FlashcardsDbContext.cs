using Flashcards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure.Presistence
{
    public class FlashcardsDbContext : DbContext
    {
        public FlashcardsDbContext(DbContextOptions<FlashcardsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FlashcardList> FlashcardLists { get; set; }

        public DbSet<Flashcard> Flashcards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
