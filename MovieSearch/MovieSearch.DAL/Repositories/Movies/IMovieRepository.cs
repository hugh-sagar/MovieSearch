using MovieSearch.DAL.Entities;

namespace MovieSearch.DAL.Repositories.Movies
{
    public interface IMovieRepository
    {
        List<Movie> RetrieveAllBySearch(string title, string genre, int page, int resultsPerPage);
    }
}
