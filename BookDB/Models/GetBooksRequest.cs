namespace BookDB.Models
{
    public class GetBooksRequest
    {
        public string? Author { get; set; }
        public int? Year { get; set; }
        public string? Publisher { get; set; }
    }
}
