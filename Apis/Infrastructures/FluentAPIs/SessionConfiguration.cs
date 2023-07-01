using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class SessionConfiguration : IEntityTypeConfiguration<BatchOfBuilding>
    {
        public void Configure(EntityTypeBuilder<BatchOfBuilding> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            // Configure relationships
            builder.HasOne(s => s.Batch).WithMany(x => x.BatchOfBuildings).HasForeignKey(s => s.BatchId);
            builder.HasOne(s => s.Building).WithMany(x => x.BatchOfBuildings).HasForeignKey(s => s.BuildingId);

        }
    }
}
