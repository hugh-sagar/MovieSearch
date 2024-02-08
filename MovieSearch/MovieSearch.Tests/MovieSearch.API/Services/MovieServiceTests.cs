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
        public void Search()
        {
            //arrange
            const string title = "title";
            const string genre = "genre";
            const int page = 1;
            const int resultsPerPage = 2;
            var model = new SearchModel { Title = title, Genre = genre, Page = page, ResultsPerPage = resultsPerPage };

            var expected = new List<Movie> { new Movie() };
            _movieRepository.Setup(x => x.RetrieveAllBySearch(title, genre, page, resultsPerPage)).Returns(expected);

            //act
            var actual = _instance.Search(model);

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        #endregion

    }
}
