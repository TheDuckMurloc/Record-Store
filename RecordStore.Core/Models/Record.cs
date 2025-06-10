namespace RecordStore.Core.Models;

public class Record
{
    public int RecordID { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string CoverImageUrl { get; set; }
    public int Year { get; set; }

    public Artist Artist { get; set; }
    public Genre Genre { get; set; }
} 