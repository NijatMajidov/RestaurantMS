using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RMS.Core.Entities;

namespace RMS.Data.DAL.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.ReservationDate).IsRequired().HasDefaultValue(DateTime.UtcNow.Date);
            builder.Property(x => x.ReservationTime).IsRequired().HasDefaultValue(TimeSpan.FromHours(DateTime.UtcNow.Hour + 4).Add(TimeSpan.FromMinutes(DateTime.UtcNow.Minute)));
            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow.AddHours(4));

        }
    }
}
