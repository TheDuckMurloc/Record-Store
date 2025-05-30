namespace RecordStore.Core.Models;

public class Artist
{
    public int ArtistID { get; set; }
    public string Name { get; set; }
    public List<Record> Records { get; set; } = new List<Record>();
} 