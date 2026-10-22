using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

namespace shared.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content {get;set;}
    public DateTime Date { get; set; } = DateTime.Now;
    public User User { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public List<Comments> Comment { get; set; } = new();
}