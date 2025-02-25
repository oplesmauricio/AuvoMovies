namespace AuvoMovies.Models.Responses
{
    public class TMBDList
    {
        public int page { get; set; }
        public List<Filme> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
