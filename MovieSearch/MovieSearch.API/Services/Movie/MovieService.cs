using System.Diagnostics;
using System.Linq.Expressions;
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
            Expression<Func<DAL.Entities.Movie, object>> orderByExpression = model.OrderBy switch
            {
                SearchModel.OrderByEnum.Title => x => x.Title,
                SearchModel.OrderByEnum.ReleaseDate => x => x.ReleaseDate,
                _ => throw new ArgumentOutOfRangeException()
            };

            return _movieRepository.RetrieveAllBySearch(model.Page, model.ResultsPerPage, orderByExpression, model.Title, model.Genre);
        }
    }
}
