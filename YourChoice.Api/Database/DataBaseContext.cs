using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using YourChoice.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourChoice.Api.Database.configuration;
using YourChoice.Domain.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace YourChoice.Api.Database
{
    public class DataBaseContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PostPart> PostParts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        //  public DbSet<VideoPost> VideoPosts { get; set; }
        //  public DbSet<PhotoPost> PhotoPosts { get; set; }
        // public DbSet<VideoPostPart> VideoPostParts { get; set; }
        // public DbSet<PhotoPostPart> PhotoPostParts { get; set; }
        public DataBaseContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YourChoicedb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostPartConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());


        }
        

    }


}
