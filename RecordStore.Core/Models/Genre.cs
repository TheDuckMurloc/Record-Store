namespace RecordStore.Core.Models;

public class Genre
{
    public int GenreID { get; set; }
    public string Name { get; set; }
    public List<Record> Records { get; set; } = new List<Record>();
} 