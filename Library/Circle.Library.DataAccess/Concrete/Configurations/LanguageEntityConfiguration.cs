using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Circle.Library.DataAccess.Concrete.Configurations
{
    public class LanguageEntityConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages", MsDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(10).IsRequired();
            builder.HasData(
                new Language 
                { 
                    Id = System.Guid.NewGuid(), 
                    Name = "Türkçe", 
                    Code = "tr-TR",
                    RecordDate = System.DateTime.Now,
                    RecordUsername = "admin",
                    Ip = "1:1",
                    UpdateDate = System.DateTime.Now,
                    UpdateUsername = "admin",
                },
                new Language 
                { 
                    Id = System.Guid.NewGuid(),
                    Name = "English", 
                    Code = "en-US" ,
                    RecordDate = System.DateTime.Now,
                    RecordUsername = "admin",
                    Ip = "1:1",
                    UpdateDate = System.DateTime.Now,
                    UpdateUsername = "admin",
                });

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