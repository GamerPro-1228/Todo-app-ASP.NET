using Microsoft.EntityFrameworkCore;

namespace endsem_project.Models
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions options):base(options)
        {        }
        public DbSet<Todo> Todo {get; set;}
    }
}