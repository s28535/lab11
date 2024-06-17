using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class MedicamentEfConfig : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.HasKey(e => e.IdMedicament).HasName("Medicament_pk");
        builder.Property(e => e.IdMedicament).ValueGeneratedNever();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
        
        builder.Property(e => e.Type).IsRequired().HasMaxLength(100);

        builder.ToTable("Medicament");
    }
}