using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Circle.Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Circle.Library.DataAccess.Concrete.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", MsDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.CategoryId);
            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Image);

            builder.Property(s => s.RecordDate)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .IsRequired();

            builder.Property(s => s.RecordUsername)
                .HasMaxLength(50)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(s => s.UpdateDate)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(s => s.UpdateUsername)
                .HasMaxLength(50)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(s => s.Ip)
                .HasMaxLength(20)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();
        }
    }
}
