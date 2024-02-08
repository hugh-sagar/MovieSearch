using System.Linq.Expressions;
using MovieSearch.DAL.Entities;

namespace MovieSearch.DAL.Repositories.Movies
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieSearchDatabaseContext _context;

        public MovieRepository(MovieSearchDatabaseContext context)
        {
            _context = context;
        }

        #region RetrieveAllBySearch

        public List<Movie> RetrieveAllBySearch(int page, int resultsPerPage, Expression<Func<Movie, object>> orderByExpression, string title = null, string genre = null)
        {
            var movieQueryable = _context.Movies.AsQueryable();
            if (string.IsNullOrWhiteSpace(title) == false) movieQueryable = movieQueryable.Where(x => x.Title != null && x.Title.ToUpper().Contains(title.ToUpper()));
            if (string.IsNullOrWhiteSpace(genre) == false) movieQueryable = movieQueryable.Where(x => x.Genre != null && x.Genre.ToUpper().Contains(genre.ToUpper()));

            return movieQueryable.OrderBy(orderByExpression)
                                 .Skip(resultsPerPage * (page - 1))
                                 .Take(resultsPerPage).ToList();
        }

        #endregion

    }
}
