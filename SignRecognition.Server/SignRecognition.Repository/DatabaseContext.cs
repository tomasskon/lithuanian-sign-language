using Microsoft.EntityFrameworkCore;
using SignRecognition.Repository.Entities;

namespace SignRecognition.Repository;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<UserEntity> Users { get; set; }
}