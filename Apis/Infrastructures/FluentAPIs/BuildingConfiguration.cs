using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.HasMany(s=>s.BatchOfBuildings).WithOne(s=>s.Building).HasForeignKey(s=>s.BuildingId);   
            builder.HasMany(s=>s.Orders).WithOne(s=>s.Building).HasForeignKey(s=>s.BuildingId);   
        }
    }
}
