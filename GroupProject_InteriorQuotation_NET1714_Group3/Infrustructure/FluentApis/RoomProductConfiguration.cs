using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentApis
{
    public class RoomProductConfiguration : IEntityTypeConfiguration<RoomProduct>
    {
        public void Configure(EntityTypeBuilder<RoomProduct> builder)
        {
            builder.HasKey(sc => new {sc.ProductId, sc.RoomId });
            builder.HasOne(x => x.Product).WithMany(x => x.RoomProducts).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(x => x.Room).WithMany(x => x.RoomProducts).HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Restrict); ;
        }
    }
    
}

