using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Circle.Library.DataAccess.Concrete.Configurations
{
    public class GroupClaimEntityConfiguration : IEntityTypeConfiguration<GroupClaim>
    {
        public void Configure(EntityTypeBuilder<GroupClaim> builder)
        {
            builder.ToTable("GroupClaims", MsDbContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.HasIndex(s => new { s.OperationClaimId, s.GroupId }).IsUnique();

            builder.Property(s => s.GroupId).IsRequired();
            builder.Property(s => s.OperationClaimId).IsRequired();

            builder
                .HasOne(s => s.Group)
                .WithMany()
                .IsRequired()
                .HasForeignKey("GroupId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(s => s.OperationClaim)
                .WithMany()
                .IsRequired()
                .HasForeignKey("OperationClaimId")
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