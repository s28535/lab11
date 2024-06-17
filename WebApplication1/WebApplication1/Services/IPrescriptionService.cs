using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    public Task<Prescription>? CreatePrescription(PrescriptionDTO prescriptionDto);
}