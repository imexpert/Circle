using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Circle.Library.DataAccess.Concrete.Configurations
{
    public class UserGroupEntityConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("UserGroups", MsDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(s => s.GroupId).IsRequired();
            builder.Property(s => s.UserId).IsRequired();

            builder
                .HasOne(s => s.Group)
                .WithMany()
                .IsRequired()
                .HasForeignKey("GroupId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(s => s.User)
                .WithMany()
                .IsRequired()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Restrict);

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