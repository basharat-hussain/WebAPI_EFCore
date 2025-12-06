using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_EFCore_Data.Models;

namespace WebAPI_EFCore_Data.Context
{
    public class APIDBContext : DbContext
    {
        public APIDBContext(DbContextOptions<APIDBContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
