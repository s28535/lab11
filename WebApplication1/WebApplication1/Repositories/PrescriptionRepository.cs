using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly HospitalDbContext _context;
    private readonly IMedicamentRepository _medRepository;
    private readonly IPatientRepository _patientRepository;

    public PrescriptionRepository(
        HospitalDbContext context, 
        IMedicamentRepository medRepository, 
        IPatientRepository patientRepository)
    {
        _context = context;
        _medRepository = medRepository;
        _patientRepository = patientRepository;
    }

    public async Task<Prescription?> CreatePrescriptionAsync(PrescriptionDTO prescriptionDTO)
    {
        var (Patient, MedicamentDtos, DateTime, DueDateTime) = prescriptionDTO;

        if (!await _patientRepository.DoesPatientExistAsync(Patient))
        {
            await _patientRepository.AddPatientAsync(Patient);
        }

        if (!_medRepository.IsNumberOfMedsLowerThanLimit(MedicamentDtos)) return null;
        if (!await _medRepository.DoAllMedsExistAsync(MedicamentDtos)) return null;

        var prescription = new Prescription
        {
            IdPrescription = GetNewId(),
            Date = DateTime,
            DueDate = DueDateTime,
            IdPatient = Patient.IdPatient,
            IdDoctor = 1
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription;
    }

    private int GetNewId()
    {
        return _context.Prescriptions.Any() ? _context.Prescriptions.Max(x => x.IdPrescription) + 1 : 1;
    }
}