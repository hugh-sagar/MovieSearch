using Microsoft.AspNetCore.Mvc;
using MovieSearch.API.Models;
using MovieSearch.API.Services.Movie;

namespace MovieSearch.API.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(SearchModel model)
        {
            if (model.Page < 1) return BadRequest("Page cannot be less than 1.");
            if (model.ResultsPerPage < 1) return BadRequest("ResultsPerPage cannot be less than 1.");

            return Json(_movieService.Search(model));
        }
    }
}
