﻿using Ali_Mav.BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ali_Mav.BlogAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts{ get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
