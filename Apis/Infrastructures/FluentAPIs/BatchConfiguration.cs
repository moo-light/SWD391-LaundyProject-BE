using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.HasOne(d => d.Driver).WithMany(p => p.Batches).HasForeignKey(d => d.DriverId);
            builder.HasMany(d => d.BatchOfBuildings).WithOne(p => p.Batch).HasForeignKey(d => d.BatchId);
            builder.HasMany(d => d.OrderInBatches).WithOne(p => p.Batch).HasForeignKey(d => d.BatchId);
        }
    }
}
