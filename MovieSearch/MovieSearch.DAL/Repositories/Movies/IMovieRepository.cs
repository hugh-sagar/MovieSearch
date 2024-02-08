using System.Linq.Expressions;
using MovieSearch.DAL.Entities;

namespace MovieSearch.DAL.Repositories.Movies
{
    public interface IMovieRepository
    {
        List<Movie> RetrieveAllBySearch(int page, int resultsPerPage, Expression<Func<Movie, object>> orderByExpression, string title = null, string genre = null);
    }
}
