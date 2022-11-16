using Microsoft.EntityFrameworkCore;
using DataGridView_Kisel.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;

namespace DataGridView_Kisel
{
    public class Context : DbContext
    {
        public DbSet<Student> BasaKisel { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("KiselDB");
            modelBuilder.Entity<Student>().HasKey(u => u.Id);
        }

    }
}
