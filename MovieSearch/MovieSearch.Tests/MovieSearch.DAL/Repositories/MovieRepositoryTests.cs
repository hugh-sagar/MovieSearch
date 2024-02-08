using FluentAssertions;
using MovieSearch.DAL.Entities;
using MovieSearch.DAL.Repositories.Movies;
using Xunit;

namespace MovieSearch.Tests.MovieSearch.DAL.Repositories
{
    public class MovieRepositoryTests : AbstractRepositoryTests
    {
        private readonly MovieRepository _instance;

        public MovieRepositoryTests()
        {
            _instance = new MovieRepository(TestDatabaseContext);
        }

        #region RetrieveAllByTitle

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RetrieveAllByTitle_WHERE_title_is_null_or_whitespace_SHOULD_filter_by_page_and_result_per_page(string title)
        {
            //arrange
            var expectedMovie1 = new Movie { Title = "c" };
            var expectedMovie2 = new Movie { Title = "d" };
            TestDatabaseContext.Movies.AddRange(new List<Movie>()
            {
                new Movie { Title = "a" },
                new Movie { Title = "b" },
                expectedMovie1,
                expectedMovie2,
                new Movie { Title = "e" },
                new Movie { Title = "f" },
            });

            TestDatabaseContext.SaveChanges();

            //act
            var actual = _instance.RetrieveAllByTitle(title, 2, 2);

            //assert
            actual.Should().HaveCount(2);
            actual.Should().Contain(x => x.Id == expectedMovie1.Id);
            actual.Should().Contain(x => x.Id == expectedMovie2.Id);
        }

        [Fact]
        public void RetrieveAllByTitle_WHERE_title_is_not_null_or_whitespace_SHOULD_filter_by_title_page_and_result_per_page()
        {
            //arrange
            const string title = "title";

            var expectedMovie1 = new Movie { Title = $"b{title}b"};
            var expectedMovie2 = new Movie { Title = title};
            var expectedMovie3 = new Movie { Title = "tItLe"};
            TestDatabaseContext.Movies.AddRange(new List<Movie>()
            {
                new Movie { Title = "a" },
                new Movie { Title = title},
                new Movie { Title = $"{title}a"},
                new Movie { Title = $"a{title}"},
                expectedMovie1,
                new Movie { Title = "b" },
                expectedMovie2,
                expectedMovie3,
                new Movie { Title = $"c{title}"},
            });
            TestDatabaseContext.SaveChanges();

            //act
            var actual = _instance.RetrieveAllByTitle(title, 2, 3);

            //assert
            actual.Should().HaveCount(3);
            actual.Should().Contain(x => x.Id == expectedMovie1.Id);
            actual.Should().Contain(x => x.Id == expectedMovie2.Id);
            actual.Should().Contain(x => x.Id == expectedMovie3.Id);
        }

        [Fact]
        public void RetrieveAllByTitle_WHERE_results_per_page_is_greater_than_total_number_of_movies_SHOULD_return_all()
        {
            //arrange
            const string title = "title";

            var expectedMovie1 = new Movie { Title = $"b{title}b" };
            var expectedMovie2 = new Movie { Title = title };
            var expectedMovie3 = new Movie { Title = "tItLe" };
            TestDatabaseContext.Movies.AddRange(new List<Movie>()
            {
                expectedMovie1,
                expectedMovie2,
                expectedMovie3,
            });
            TestDatabaseContext.SaveChanges();

            //act
            var actual = _instance.RetrieveAllByTitle(title, 1, 300);

            //assert
            actual.Should().HaveCount(3);
            actual.Should().Contain(x => x.Id == expectedMovie1.Id);
            actual.Should().Contain(x => x.Id == expectedMovie2.Id);
            actual.Should().Contain(x => x.Id == expectedMovie3.Id);
        }

        #endregion


    }
}
