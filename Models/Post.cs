using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

namespace MiniProject.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public User User { get; set; }
    public int Vote { get; set; }
    public List<Comments> Comment { get; set; } = new();
}