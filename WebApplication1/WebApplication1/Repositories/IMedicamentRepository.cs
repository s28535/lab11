using WebApplication1.DTO;

namespace WebApplication1.Repositories;

public interface IMedicamentRepository
{
    bool IsNumberOfMedsLowerThanLimit(List<MedicamentDTO> medicaments);
    Task<bool> DoAllMedsExistAsync(List<MedicamentDTO> medicaments);
}