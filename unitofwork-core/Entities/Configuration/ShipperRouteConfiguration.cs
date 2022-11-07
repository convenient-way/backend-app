using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class ShipperRouteConfiguration : IEntityTypeConfiguration<ShipperRoute>
    {
        public void Configure(EntityTypeBuilder<ShipperRoute> builder)
        {
            builder.ToTable("ShipperRoute");

        }
    }
}
