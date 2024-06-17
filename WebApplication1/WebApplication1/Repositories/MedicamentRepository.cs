using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class MedicamentRepository : IMedicamentRepository
{
    private readonly HospitalDbContext _context;

    public MedicamentRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public bool IsNumberOfMedsLowerThanLimit(List<MedicamentDTO> medicaments)
    {
        return medicaments.Count < 10;
    }

    public async Task<bool> DoAllMedsExistAsync(List<MedicamentDTO> medicaments)
    {
        var medIds = medicaments.Select(x => x.IdMedicament).ToArray();
        var meds = await _context.Medicaments
            .Where(x => medIds.Contains(x.IdMedicament))
            .ToListAsync();

        return medicaments.Count == meds.Count;
    }
}