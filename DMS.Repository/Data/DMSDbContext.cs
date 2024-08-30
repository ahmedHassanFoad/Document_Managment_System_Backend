using DMS.Core.Entities;
using DMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Directory = DMS.Core.Entities.Directory;

namespace DMS.Repository.Data
{
    public class DMSDbContext : IdentityDbContext <AppUser>
    {
        public DMSDbContext(DbContextOptions<DMSDbContext> Options) : base(Options)
        {

        }
        public DbSet<WorkSpace> WorkSpaces { get; set; }
        public DbSet<Core.Entities.Directory> Directories { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-One relationship between AppUser and WorkSpace



            // One-to-Many relationship between WorkSpace and Directory
            modelBuilder.Entity<Directory>()
            .HasKey(d => d.Id);
            modelBuilder.Entity<Directory>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<WorkSpace>()
            .HasMany(w => w.Directories)
            .WithOne(d => d.WorkSpace)            
            .OnDelete(DeleteBehavior.Cascade);
            //primary key 
            modelBuilder.Entity<Directory>()
            .HasKey(d => d.Id);

            modelBuilder.Entity<Directory>().Property(d => d.Id).ValueGeneratedOnAdd();
            // One-to-Many relationship between Directory and Document
            modelBuilder.Entity<Directory>()
                .HasMany(d => d.Documents)
                .WithOne(doc => doc.Directory)             
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Directory>()
                .Property(b => b.IsPublic).HasDefaultValue(false);
            modelBuilder.Entity<Directory>()
                .Property(b => b.IsDeleted).HasDefaultValue(false);


            modelBuilder.Entity<Document>()
                .HasKey(doc => doc.Id);
            modelBuilder.Entity<Document>()
                .Property(b => b.IsDeleted).HasDefaultValue(false);
            ;
        }
    }
}

