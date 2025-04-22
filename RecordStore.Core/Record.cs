namespace RecordStore.Core;

public class Record
{
    public int RecordID { get; set; }
    public string Title { get; set; }
    public string ArtistID { get; set; }
    public string GenreID { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string CoverImageUrl { get; set; }
}
