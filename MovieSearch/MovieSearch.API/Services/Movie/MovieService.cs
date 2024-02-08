using MovieSearch.API.Models;
using MovieSearch.DAL.Repositories.Movies;

namespace MovieSearch.API.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public List<DAL.Entities.Movie> Search(SearchModel model)
        {
            return _movieRepository.RetrieveAllBySearch(model.Title, model.Genre, model.Page, model.ResultsPerPage);
        }
    }
}
