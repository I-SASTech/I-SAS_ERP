using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class NobleRolesPermissionsConfiguration : IEntityTypeConfiguration<NobleRolePermission>
    {
        public void Configure(EntityTypeBuilder<NobleRolePermission> builder)
        {
            builder.HasOne(a => a.NobleRole)
                .WithMany(j => j.NobleRolePermissions)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.CompanyPermissions)
                .WithMany(j => j.NobleRolePermissions)
                .HasForeignKey(a => a.PermissionId);
        }
    }
}