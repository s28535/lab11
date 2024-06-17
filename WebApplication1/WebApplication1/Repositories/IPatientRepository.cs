using WebApplication1.Entities;

namespace WebApplication1.Repositories;


public interface IPatientRepository
{
    Task<bool> DoesPatientExistAsync(Patient patient);
    Task AddPatientAsync(Patient patient);
}