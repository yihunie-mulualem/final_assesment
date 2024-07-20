using Microsoft.EntityFrameworkCore;
using Training_Assesment.Model;
namespace Training_Assesment.DBContext
{
    public class TrainingDbContext: DbContext
    {
        public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
        {
        }
        public DbSet<Course> courses { get; set; }
        public DbSet<Trainee> trainees { get; set; }
        public DbSet<Trainner> trainners { get; set; }
    
    }
}
