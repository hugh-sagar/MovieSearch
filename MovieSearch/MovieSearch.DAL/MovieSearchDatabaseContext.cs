using Microsoft.EntityFrameworkCore;
using MovieSearch.DAL.Entities;

namespace MovieSearch.DAL
{
    public class MovieSearchDatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieSearchDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.ReleaseDate).HasColumnName("Release_Date");
                x.Property(y => y.Title).HasColumnName("Title");
                x.Property(y => y.Overview).HasColumnName("Overview");
                x.Property(y => y.Popularity).HasColumnName("Popularity");
                x.Property(y => y.VoteCount).HasColumnName("Vote_Count");
                x.Property(y => y.VoteAverage).HasColumnName("Vote_Average");
                x.Property(y => y.OriginalLanguage).HasColumnName("Original_Language");
                x.Property(y => y.Genre).HasColumnName("Genre");
                x.Property(y => y.PosterUrl).HasColumnName("Poster_Url");
            });
        }
    }
}
