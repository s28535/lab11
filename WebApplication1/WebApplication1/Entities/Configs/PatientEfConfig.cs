using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class PatientEfConfig : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(e => e.IdPatient).HasName("Patient_pk");
        builder.Property(e => e.IdPatient).ValueGeneratedNever();

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);

        builder.Property(e => e.BirthDate).IsRequired();

        builder.ToTable("Patient");
    }
}