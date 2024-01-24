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
    public class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
    {
        public void Configure(EntityTypeBuilder<Quotation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany<Payment>(x => x.Payments).WithOne(x => x.Quotation);
            builder.HasMany<Room>(x => x.Rooms).WithOne(x => x.Quotation);
            builder.HasMany<Product>(x => x.Products).WithOne(x => x.Quotation);
        }
    
    }
}
