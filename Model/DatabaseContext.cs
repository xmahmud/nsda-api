using Microsoft.EntityFrameworkCore;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Db;

namespace Model
{
    public class DatabaseContext : BaseDbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> User { set; get; }
        public DbSet<Token> Token { set; get; }
        public DbSet<Transaction> Transaction { set; get; }
        //public DbSet<ForgetPassword> ForgetPassword { set; get; }
        //public DbSet<EmailTemplate> EmailTemplate { set; get; }
        //public DbSet<Media> Media { set; get; }
        //public DbSet<MediaInfo> MediaInfo { set; get; }
        //public DbSet<UserMedia> UserMedia { set; get; }
        //public DbSet<VerificationCode> VerificationCode { set; get; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Media>()
            // .HasOne(m => m.Small)
            // .WithMany(m => m.SmallSizes)
            // .HasForeignKey(m => m.SmallId)
            // .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Media>()
            // .HasOne(m => m.Medium)
            // .WithMany(m => m.MediumSizes)
            // .HasForeignKey(m => m.MediumId)
            // .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Media>()
            // .HasOne(m => m.Large)
            // .WithMany(m => m.LargeSizes)
            // .HasForeignKey(m => m.LargeId)
            // .OnDelete(DeleteBehavior.Restrict);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
