using Microsoft.EntityFrameworkCore;
using WebApplicationZno.Models;

namespace WebApplicationZno.Persistance
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {

        }
        public DbSet<QuestionModel> Questions { get; set; }
    }
}
