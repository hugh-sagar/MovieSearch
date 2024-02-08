using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieSearch.API.Controllers;
using MovieSearch.API.Models;
using MovieSearch.API.Services.ErrorReporting;
using MovieSearch.API.Services.Movie;
using MovieSearch.DAL.Entities;
using Xunit;

namespace MovieSearch.Tests.MovieSearch.API.Controllers
{
    public class MovieControllerTests
    {
        private readonly MovieController _instance;
        private readonly Mock<IMovieService> _movieService;
        private readonly Mock<IErrorReportingService> _errorReportingService;

        public MovieControllerTests()
        {
            _movieService = new Mock<IMovieService>();
            _errorReportingService = new Mock<IErrorReportingService>();
            _instance = new MovieController(_movieService.Object, _errorReportingService.Object);
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
        public void Search_WHERE_an_exception_is_thrown_SHOULD_report_error_and_return_status_500()
        {
            //arrange
            var model = new SearchModel { Page = 1, ResultsPerPage = 1 };

            var exception = new Exception();
            _movieService.Setup(x => x.Search(model)).Throws(exception);

            //act
            var actual = _instance.Search(model);

            //assert
            var result = actual as StatusCodeResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);

            _errorReportingService.Verify(x => x.ReportError(exception), Times.Once);
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
