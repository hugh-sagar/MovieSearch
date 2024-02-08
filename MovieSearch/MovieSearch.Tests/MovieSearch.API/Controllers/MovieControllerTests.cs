using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieSearch.API.Controllers;
using MovieSearch.API.Models;
using MovieSearch.API.Services.Movie;
using MovieSearch.DAL.Entities;
using Xunit;

namespace MovieSearch.Tests.MovieSearch.API.Controllers
{
    public class MovieControllerTests
    {
        private readonly MovieController _instance;
        private readonly Mock<IMovieService> _movieService;

        public MovieControllerTests()
        {
            _movieService = new Mock<IMovieService>();
            _instance = new MovieController(_movieService.Object);
        }

        #region Search

        [Fact]
        public void Search_WHERE_Page_is_less_than_one_SHOULD_return_a_BadRequest()
        {
            //arrange
            var model = new SearchModel { Page = 0 };

            //act
            var actual = _instance.Search(model);

            //assert
            var result = actual as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be("Page cannot be less than 1.");
        }

        [Fact]
        public void Search_WHERE_ResultsPerPage_is_less_than_one_SHOULD_return_a_BadRequest()
        {
            //arrange
            var model = new SearchModel { Page = 1, ResultsPerPage = 0 };

            //act
            var actual = _instance.Search(model);

            //assert
            var result = actual as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be("ResultsPerPage cannot be less than 1.");
        }

        [Fact]
        public void Search()
        {
            //arrange
            var model = new SearchModel { Page = 1, ResultsPerPage = 1 };

            var expected = new List<Movie>{ new() };
            _movieService.Setup(x => x.Search(model)).Returns(expected);

            //act
            var actual = _instance.Search(model);

            //assert
            var result = actual as JsonResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }

        #endregion

    }
}
