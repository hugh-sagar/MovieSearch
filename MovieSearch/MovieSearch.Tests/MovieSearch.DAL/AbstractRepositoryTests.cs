using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieSearch.DAL;

namespace MovieSearch.Tests.MovieSearch.DAL
{
    public class AbstractRepositoryTests
    {
        public MovieSearchDatabaseContext TestDatabaseContext;

        public AbstractRepositoryTests()
        {
            var connectionOptions = new DbContextOptionsBuilder<MovieSearchDatabaseContext>().UseSqlite(new SqliteConnection("Filename=:memory:")).Options;
            TestDatabaseContext = new MovieSearchDatabaseContext(connectionOptions);

            var connection = TestDatabaseContext.Database.GetDbConnection();
            connection.Close();
            connection.Open();

            TestDatabaseContext.Database.EnsureDeleted();
            TestDatabaseContext.Database.EnsureCreated();
        }
    }
}
