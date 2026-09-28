using System;

namespace MiniProject.Models;

public class Comments
{
    public int Id { get; set; }
    public string Text { get; set; }
    public User User { get; set; }
    public DateTime Date { get; set; }
    public int Vote { get; set; }
}