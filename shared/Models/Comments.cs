using System; 

namespace shared.Models;

public class Comments
{
    public int Id { get; set; }
    public string Text { get; set; }
    public User User { get; set; }
    public DateTime Date { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
}