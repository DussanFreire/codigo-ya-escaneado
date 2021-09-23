using BackEndAlbergue.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using BackEndAlbergue.Data.Entities;

namespace BackEndAlbergue.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<PetEntity> pets { get; set; }
        public DbSet<NoticeModel> notice { get; set; }
        public DbSet<ProductModel> petShop { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
