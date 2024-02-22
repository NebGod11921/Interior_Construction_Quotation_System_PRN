using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentApis
{
    public class ProductQuotationConfiguration : IEntityTypeConfiguration<ProductQuotation>
    {
        public void Configure(EntityTypeBuilder<ProductQuotation> builder)
        {
            //adding product quotation relationship n - n
            builder.HasKey(sc => new { sc.ProductId, sc.QuotationId });
            builder.HasOne(x => x.Product).WithMany(x => x.ProductQuotations).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(x => x.Quotation).WithMany(x => x.ProductQuotations).HasForeignKey(x => x.QuotationId).OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}
