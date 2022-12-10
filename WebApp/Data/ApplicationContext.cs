using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Category {get;set;}
        public DbSet<ApplicationType> ApplicationType {get;set;}
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
    }
}
