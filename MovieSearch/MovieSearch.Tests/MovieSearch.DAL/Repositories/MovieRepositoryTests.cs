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

        #region RetrieveAllBySearch

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RetrieveAllBySearch_WHERE_title_and_genre_are_null_or_whitespace_SHOULD_order_and_filter_by_page_and_result_per_page(string searchField)
        {
            //arrange
            var expectedMovie1 = new Movie { Title = "c" };
            var expectedMovie2 = new Movie { Title = "d" };
            TestDatabaseContext.Movies.AddRange(new List<Movie>()
            {
                expectedMovie1,
                new Movie { Title = "f" },
                new Movie { Title = "e" },
                new Movie { Title = "a" },
                new Movie { Title = "b" },
                expectedMovie2,
            });

            TestDatabaseContext.SaveChanges();

            //act
            var actual = _instance.RetrieveAllBySearch(2, 2, x => x.Title, searchField, searchField);

            //assert
            actual.Should().HaveCount(2);
            actual[0].Id.Should().Be(expectedMovie1.Id);
            actual[1].Id.Should().Be(expectedMovie2.Id);
        }

        [Fact]
        public void RetrieveAllBySearch_WHERE_title_is_not_null_or_whitespace_SHOULD_order_and_filter_by_title_page_and_result_per_page()
        {
            //arrange
            const string title = "title";

            var expectedMovie1 = new Movie { Title = $"d{title}"};
            var expectedMovie2 = new Movie { Title = title};
            var expectedMovie3 = new Movie { Title = "ztItLe"};
            TestDatabaseContext.Movies.AddRange(new List<Movie>()
            {
                new Movie { Title = "a" },
                new Movie { Title = $"c{title}"},
                new Movie { Title = $"b{title}"},
                expectedMovie1,
                new Movie { Title = $"a{title}"},
                new Movie { Title = "b" },
                expectedMovie2,
                new Movie { Title = $"z{title}"},
                expectedMovie3,
            });
            TestDatabaseContext.SaveChanges();

            //act
            var actual = _instance.RetrieveAllBySearch(2, 3, x => x.Title, title);

            //assert
            actual.Should().HaveCount(3);
            actual[0].Id.Should().Be(expectedMovie1.Id);
            actual[1].Id.Should().Be(expectedMovie2.Id);
            actual[2].Id.Should().Be(expectedMovie3.Id);
        }

        [Fact]
        public void RetrieveAllBySearch_WHERE_genre_is_not_null_or_whitespace_SHOULD_order_and_filter_by_genre_page_and_result_per_page()
        {
            //arrange
            const string genre = "genre";

            var expectedMovie1 = new Movie { Genre = genre };
            var expectedMovie2 = new Movie { Genre = $"a{genre}a" };
            var expectedMovie3 = new Movie { Genre = "gEnRe" };
            TestDatabaseContext.Movies.AddRange(new List<Movie>()
            {
                new Movie { Genre = "a" },
                new Movie { Genre = genre },
                new Movie { Genre = $"a{genre}b"},
                new Movie { Genre = genre.ToUpper()},
                expectedMovie1,
                new Movie(),
                expectedMovie2,
                expectedMovie3,
                new Movie { Genre = genre},
            });
            TestDatabaseContext.SaveChanges();

            //act
            var actual = _instance.RetrieveAllBySearch(2, 3, x => x.Title, genre: genre);

            //assert
            actual.Should().HaveCount(3);
            actual[0].Id.Should().Be(expectedMovie1.Id);
            actual[1].Id.Should().Be(expectedMovie2.Id);
            actual[2].Id.Should().Be(expectedMovie3.Id);
        }

        [Fact]
        public void RetrieveAllBySearch_WHERE_results_per_page_is_greater_than_total_number_of_movies_SHOULD_return_all_results_ordered()
        {
            //arrange
            var expectedMovie1 = new Movie { Title = "a" };
            var expectedMovie2 = new Movie { Title = "b" };
            var expectedMovie3 = new Movie { Title = "c" };
            TestDatabaseContext.Movies.AddRange(new List<Movie>()
            {
                expectedMovie2,
                expectedMovie3,
                expectedMovie1,
            });
            TestDatabaseContext.SaveChanges();

            //act
            var actual = _instance.RetrieveAllBySearch(1, 300, x => x.Title);

            //assert
            actual.Should().HaveCount(3);
            actual[0].Id.Should().Be(expectedMovie1.Id);
            actual[1].Id.Should().Be(expectedMovie2.Id);
            actual[2].Id.Should().Be(expectedMovie3.Id);
        }

        #endregion

    }
}
