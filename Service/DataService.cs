using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using MiniProject.Data;
using MiniProject.Models;

namespace MiniProject.Service;

public class DataService
{
   private PostContext db { get; }

   public DataService(PostContext db)
   {
      this.db = db;
   }

   public void SeedData()
   {
      User user = db.Users.FirstOrDefault()!;

      if (user == null)
      {
         user = new User { Name = "Jacob" };
         db.Users.Add(user);
         db.Users.Add(new User { Name = "Casper" });
         db.Users.Add(new User { Name = "Oliver" });

      }

      Post post = db.Posts.FirstOrDefault()!;
      if (post == null)
      {
         List<Comments> commentsList = new List<Comments>()
         {
            new Comments
            {
               Text = "Test", User = user, Date = DateTime.Now, Vote = 0
            },
            new Comments
            {
               Text = "Test 2", User = user, Date = DateTime.Now, Vote = 2
            },
            new Comments
            {
               Text = "Test 3", User = user, Date = DateTime.Now, Vote = 5
            }
         };

         db.Posts.Add(new Post
         {
            Date = DateTime.Now,
            User = user,
            Vote = 0,
            Comment = commentsList,
            Title = "Post test"
         });
      }

      db.SaveChanges();
   }

   public List<Post> GetPosts()
   {
      return db.Posts.Include(p => p.User).ToList();
   }

   public List<User> GetUsers()
   {
      return db.Users.ToList();
   }
}
   