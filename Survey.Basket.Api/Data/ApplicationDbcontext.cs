﻿using Microsoft.EntityFrameworkCore;
using Survey.Basket.Api.Data.Entites;
using System.Reflection;

namespace Survey.Basket.Api.Data
{
    public class ApplicationDbcontext:DbContext
    {

        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> optionsBuilder):base(optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
