using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Context
{
    public class NewsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=newsdb;user=root;password=");
        }

        public DbSet<Author> Author { get; set; }

        public DbSet<Rubric> Rubric { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<NewsTag> NewsTag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(ent => ent.Id);

                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();

                entity.Property(ent => ent.Username).IsRequired();

                entity.Property(ent => ent.Email).IsRequired();

                entity.Property(ent => ent.Password).IsRequired();
            });

            modelBuilder.Entity<Rubric>(entity =>
            {
                entity.HasKey(ent => ent.Id);

                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();

                entity.Property(ent => ent.Name).IsRequired();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(ent => ent.Id);

                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();

                entity.Property(ent => ent.Name).IsRequired();
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(ent => ent.Id);

                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();

                entity.Property(ent => ent.Date).IsRequired();

                entity.Property(ent => ent.Title).IsRequired();

                entity.Property(ent => ent.Content).IsRequired();

                entity.HasOne<Author>(ent => ent.Author)
                .WithMany(ent => ent.News)
                .HasForeignKey(ent => ent.AuthorId);

                entity.HasOne<Rubric>(ent => ent.Rubric)
                .WithMany(ent => ent.News)
                .HasForeignKey(ent => ent.RubricId);
            });

            modelBuilder.Entity<NewsTag>(entity =>
            {
                entity.HasKey(ent => ent.Id);

                entity.Property(ent => ent.Id).ValueGeneratedOnAdd();

                entity.HasOne<News>(ent => ent.News)
                .WithMany(ent => ent.Tags)
                .HasForeignKey(ent => ent.NewsId);

                entity.HasOne<Tag>(ent => ent.Tag)
                .WithMany(ent => ent.Tags)
                .HasForeignKey(ent => ent.TagId);
            });
        }
    }
}
