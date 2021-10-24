using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Circle.Library.DataAccess.Concrete.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", MsDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.MobilePhones).IsUnique(true);
            builder.HasIndex(x => x.Email).IsUnique(true);

            builder.Property(x => x.DepartmentId).IsRequired();
            builder.Property(x => x.Firstname).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Lastname).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Password).HasMaxLength(50);
            builder.Property(x => x.RefreshToken).HasMaxLength(100);
            builder.Property(x => x.Image);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.BirthDate);
            builder.Property(x => x.Gender);
            builder.Property(x => x.RecordDate);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.MobilePhones).HasMaxLength(30);
            builder.Property(x => x.Notes).HasMaxLength(500);

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