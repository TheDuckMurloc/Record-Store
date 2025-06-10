namespace RecordStore.Core.Models;

public class CartItem
{
    public int RecordId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total => Price * Quantity;
} 
