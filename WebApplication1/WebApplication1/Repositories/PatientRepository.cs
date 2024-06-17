using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly HospitalDbContext _context;

    public PatientRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesPatientExistAsync(Patient patient)
    {
        return await _context.Patients.FindAsync(patient.IdPatient) != null;
    }

    public async Task AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }
}