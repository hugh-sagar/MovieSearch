using MovieSearch.DAL.Entities;

namespace MovieSearch.DAL.Repositories.Movies
{
    public interface IMovieRepository
    {
        List<Movie> RetrieveAllByTitle(string title, int page, int resultsPerPage);
    }
}
