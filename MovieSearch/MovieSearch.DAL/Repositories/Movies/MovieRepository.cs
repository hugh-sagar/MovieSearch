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

        #region RetrieveAllByTitle

        public List<Movie> RetrieveAllByTitle(string title, int page, int resultsPerPage)
        {
            var movieQueryable = string.IsNullOrWhiteSpace(title) ? _context.Movies.AsQueryable()
                                                                  : _context.Movies.Where(x => x.Title != null && x.Title.ToUpper().Contains(title.ToUpper()));

            return movieQueryable.Skip(resultsPerPage * (page - 1))
                                 .Take(resultsPerPage).ToList();
        }

        #endregion
    }
}
