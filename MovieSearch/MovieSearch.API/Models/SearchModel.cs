namespace MovieSearch.API.Models
{
    public class SearchModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ResultsPerPage { get; set; }
        public int Page { get; set; }
    }
}
