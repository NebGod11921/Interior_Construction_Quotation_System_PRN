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
            builder.Property(x => x.Id).ValueGeneratedOnAddOrUpdate();
            builder.HasMany<Payment>(x => x.Payments).WithOne(x => x.Quotation);

            //fixing room relationship 1 - 1
            builder.HasOne<Room>(x => x.Room).WithOne(x => x.Quotation);

        }

    }
}
