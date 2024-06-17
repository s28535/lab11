using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class DoctorEfConfig : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(e => e.IdDoctor).HasName("Doctor_pk");
        builder.Property(e => e.IdDoctor).ValueGeneratedNever();

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);

        builder.ToTable("Doctor");
    }
}