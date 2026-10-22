using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using MiniProject.Data;
using shared.Models;
using SQLitePCL;

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
         user = new User { Name = "Jacob", Id = 1};
         db.Users.Add(user);
         db.Users.Add(new User { Name = "Casper" });
         db.Users.Add(new User { Name = "Oliver" });

      }

      Post post = db.Posts.FirstOrDefault()!;
      if (post == null)
      {
         db.Posts.Add(new Post
         {
            Date = DateTime.Now,
            User = user,
            Upvotes = 5,
            Downvotes = 2,
            Content = "Dette er et test Post",
            Comment = new List<Comments>()
            {
               new Comments
               {
                  Text = "Test", User = user, Date = DateTime.Now, Upvotes = 1, Downvotes = 0
               },
               new Comments
               {
                  Text = "Test 2", User = user, Date = DateTime.Now, Upvotes = 3, Downvotes = 0
               },
               new Comments
               {
                  Text = "Test 3", User = user, Date = DateTime.Now, Upvotes = 5, Downvotes = 3
               }
            },
            Title = "Post test"
         });
      }

      db.SaveChanges();
   }

   public List<Post> GetPosts()
   {
      return db.Posts
         .Include(p => p.User)
         .Include(p => p.Comment)
         .ToList();
   }

   public Post? GetPost(int id)
   {
      return db.Posts
         .Include(p => p.User)
         .Include(p => p.Comment)
               .ThenInclude(c => c.User)
         .FirstOrDefault(p => p.Id == id);
   }

   public string CreatePost(string Title, string Content, int UserId)
   {
      User? user = db.Users.FirstOrDefault(u => u.Id == UserId);
      db.Posts.Add(new Post { Title = Title, Content = Content, User = user });
      db.SaveChanges();
      return "Post created";
   }

   public string CreateComment(string content, int userId, int postId)
   {
      User? user = db.Users.FirstOrDefault(u => u.Id == userId);
      Post? post = db.Posts.FirstOrDefault(p => p.Id == postId);
      
      if (user == null || post == null)
      {
        return "User or Post not found";
      } 

      Comments comment = new Comments
      {
        Text = content,
        User = user,
      };

      post.Comment.Add(comment);

      db.SaveChanges();
      return "Comment Created";
   }

   public string PostUpvote(int postid)
   {
      Post? post = db.Posts.FirstOrDefault(p => p.Id == postid);

      post.Upvotes++;
      
      db.SaveChanges();

      return "Upvote made";
   }
   
   public string PostDownvote(int postid)
   {
      Post? post = db.Posts.FirstOrDefault(p => p.Id == postid);

      post.Downvotes++;
      
      db.SaveChanges();

      return "Downvote made";
   }
   
   public string PostCommentUpvote(int postid, int commentid)
   {
      Post? post = db.Posts
        .Include(p => p.Comment) 
        .FirstOrDefault(p => p.Id == postid);
      Comments comment = post.Comment.FirstOrDefault(c => c.Id == commentid);
         
      comment.Upvotes++;
      
      db.SaveChanges();

      return "Comment upvote made";
   }
   
   public string PostCommentDownvote(int postid, int commentid)
   {
      Post? post = db.Posts
        .Include(p => p.Comment) 
        .FirstOrDefault(p => p.Id == postid);
      Comments? comment = post.Comment.FirstOrDefault(c => c.Id == commentid);
         
      comment.Downvotes++;
      
      db.SaveChanges();

      return "Comment downvote made";
   }
}