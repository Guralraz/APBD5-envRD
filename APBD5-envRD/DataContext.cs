namespace APBD5_envRD;

using Microsoft.EntityFrameworkCore;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Animal> Animals { get; set; }
}
