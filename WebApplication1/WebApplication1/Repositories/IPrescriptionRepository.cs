using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public interface IPrescriptionRepository
{
    Task<Prescription?> CreatePrescriptionAsync(PrescriptionDTO prescription);
}