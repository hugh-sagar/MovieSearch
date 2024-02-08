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

        public List<Movie> RetrieveAllBySearch(string title, string genre, int page, int resultsPerPage)
        {
            var movieQueryable = _context.Movies.AsQueryable();
            if (string.IsNullOrWhiteSpace(title) == false) movieQueryable = movieQueryable.Where(x => x.Title != null && x.Title.ToUpper().Contains(title.ToUpper()));
            if (string.IsNullOrWhiteSpace(genre) == false) movieQueryable = movieQueryable.Where(x => x.Genre != null && x.Genre.ToUpper().Contains(genre.ToUpper()));

            return movieQueryable.Skip(resultsPerPage * (page - 1))
                                 .Take(resultsPerPage).ToList();
        }

        #endregion

    }
}
