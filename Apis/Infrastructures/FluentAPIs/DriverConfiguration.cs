﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            //builder.Property(x => x.Email).HasMaxLength(100);
            //builder.HasIndex(e => e.Email).IsUnique();
            //builder.Property(e => e.FullName).HasMaxLength(100);
            //builder.Property(e => e.PhoneNumber).HasMaxLength(20);

            builder.HasMany(x => x.Batches).WithOne(x => x.Driver).HasForeignKey(x => x.DriverId);

        }
    }
}
