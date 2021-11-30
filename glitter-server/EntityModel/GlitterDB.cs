using DAL.Entities;
using EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    
    public class GlitterDB : DbContext
    {
        public GlitterDB() : base("BlogDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GlitterDB, Migrations.Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TweetLikeDislike> Likes { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<FollowingUser> UserLinks { get; set; }

        public DbSet<Search> Searchs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
