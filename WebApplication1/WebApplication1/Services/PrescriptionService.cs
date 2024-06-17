using WebApplication1.DTO;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _repository;

    public PrescriptionService(IPrescriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Prescription?> CreatePrescription(PrescriptionDTO prescriptionDTO)
    {
        if (prescriptionDTO.DateTime > prescriptionDTO.DueDateTime) return null;
        return await _repository.CreatePrescriptionAsync(prescriptionDTO);
    }
}