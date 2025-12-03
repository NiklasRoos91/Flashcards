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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<FlashcardTag> FlashcardTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key for the pivot table
            modelBuilder.Entity<FlashcardTag>()
                .HasKey(ft => new { ft.FlashcardId, ft.TagId });

            // Configure relationship: Flashcard -> FlashcardTag
            modelBuilder.Entity<FlashcardTag>()
                .HasOne(ft => ft.Flashcard)
                .WithMany(f => f.FlashcardTags)
                .HasForeignKey(ft => ft.FlashcardId);

            // Configure relationship: Tag -> FlashcardTag
            modelBuilder.Entity<FlashcardTag>()
                .HasOne(ft => ft.Tag)
                .WithMany(t => t.FlashcardTags)
                .HasForeignKey(ft => ft.TagId);

            // Cascade delete for User -> FlashcardLists
            modelBuilder.Entity<User>()
                .HasMany(u => u.FlashcardLists)
                .WithOne(fl => fl.User)
                .HasForeignKey(fl => fl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade delete for FlashcardList -> Flashcards
            modelBuilder.Entity<FlashcardList>()
                .HasMany(fl => fl.Flashcards)
                .WithOne(f => f.FlashcardList)
                .HasForeignKey(f => f.FlashcardListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
