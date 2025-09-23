using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIA.Client.API.Models
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "Control Panel",
                    RoleDescription = "Admin role with full permissions"
                },
                new Role
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "User Panel ",
                    RoleDescription = "Standard user role with limited permissions"
                }
            );
        }
    }
}
