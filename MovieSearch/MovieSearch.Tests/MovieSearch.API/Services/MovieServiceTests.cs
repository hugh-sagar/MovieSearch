using FluentAssertions;
using Moq;
using MovieSearch.API.Models;
using MovieSearch.API.Services.Movie;
using MovieSearch.DAL.Entities;
using MovieSearch.DAL.Repositories.Movies;
using Xunit;

namespace MovieSearch.Tests.MovieSearch.API.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _movieRepository;
        private readonly MovieService _instance;

        public MovieServiceTests()
        {
            _movieRepository = new Mock<IMovieRepository>();
            _instance = new MovieService(_movieRepository.Object);
        }

        #region Search

        [Fact]
        public void Search_WHERE_OrderBy_is_Title_SHOULD_return_RetrieveAllBySearch_ordering_by_title()
        {
            //arrange
            const string title = "title";
            const string genre = "genre";
            const int page = 1;
            const int resultsPerPage = 2;
            var model = new SearchModel { Title = title, Genre = genre, Page = page, ResultsPerPage = resultsPerPage, OrderBy = SearchModel.OrderByEnum.Title};

            var expected = new List<Movie> { new Movie() };
            _movieRepository.Setup(x => x.RetrieveAllBySearch(page, resultsPerPage, x => x.Title, title, genre)).Returns(expected);

            //act
            var actual = _instance.Search(model);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Search_WHERE_OrderBy_is_ReleaseDate_SHOULD_return_RetrieveAllBySearch_ordering_by_release_date()
        {
            //arrange
            const string title = "title";
            const string genre = "genre";
            const int page = 1;
            const int resultsPerPage = 2;
            var model = new SearchModel { Title = title, Genre = genre, Page = page, ResultsPerPage = resultsPerPage, OrderBy = SearchModel.OrderByEnum.ReleaseDate };

            var expected = new List<Movie> { new Movie() };
            _movieRepository.Setup(x => x.RetrieveAllBySearch(page, resultsPerPage, x => x.ReleaseDate, title, genre)).Returns(expected);

            //act
            var actual = _instance.Search(model);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Search_WHERE_OrderBy_is_out_of_range_SHOULD_throw_exception()
        {
            //arrange
            const string title = "title";
            const string genre = "genre";
            const int page = 1;
            const int resultsPerPage = 2;
            var model = new SearchModel { Title = title, Genre = genre, Page = page, ResultsPerPage = resultsPerPage, OrderBy = (SearchModel.OrderByEnum)100 };

            //act + assert
            _instance.Invoking(x => x.Search(model))
                     .Should().Throw<ArgumentOutOfRangeException>();
        }

        #endregion

    }
}
