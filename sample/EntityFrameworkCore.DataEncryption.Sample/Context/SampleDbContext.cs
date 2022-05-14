using EntityFrameworkCore.DataEncryption.Conversions;
using EntityFrameworkCore.DataEncryption.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.DataEncryption.Sample.Context;

/// <summary>
/// Sample Db Context
/// </summary>
public class SampleDbContext : DbContext
{
    /// <summary>
    /// protected ctor
    /// </summary>
    protected SampleDbContext()
    {
    }

    /// <summary>
    /// public ctor
    /// </summary>
    /// <param name="options">
    /// DbContext Options object. <see cref="DbContextOptions{TContext}"/>
    /// </param>
    public SampleDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity
                .Property(p => p.Id)
                .UseIdentityColumn();

            entity
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(2048);
            
            entity
                .Property(p => p.Surname)
                .IsRequired()
                .HasMaxLength(2048);

            entity
                .Property(p => p.Phone)
                .IsRequired()
                .HasConversion(new EncryptValueConverter("89acMXSBpuEBDWHZ"));
        });
        base.OnModelCreating(modelBuilder);
    }
}