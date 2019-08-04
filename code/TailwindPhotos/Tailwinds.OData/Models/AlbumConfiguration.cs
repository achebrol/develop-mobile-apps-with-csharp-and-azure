using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tailwinds.OData.Models
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(p => p.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()");

            builder
                .Property(p => p.LastUpdated)
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("DATETIMEOFFSET")
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .Property(p => p.Deleted)
                .HasColumnType("BIT")
                .HasDefaultValue(0);
        }
    }
}
