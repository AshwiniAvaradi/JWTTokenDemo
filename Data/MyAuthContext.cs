using JWTTokenDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTTokenDemo.Data
{
    public class MyAuthContext : DbContext
    {


        public MyAuthContext(DbContextOptions<MyAuthContext> context) : base(context)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
