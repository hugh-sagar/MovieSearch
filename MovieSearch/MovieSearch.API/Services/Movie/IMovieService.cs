using MovieSearch.API.Models;

namespace MovieSearch.API.Services.Movie
{
    public interface IMovieService
    {
        List<DAL.Entities.Movie> Search(SearchModel model);
    }
}
