using WebApplication1.Entities;

namespace WebApplication1.DTO;

public record PrescriptionDTO(Patient Patient, List<MedicamentDTO> MedicamentDtos, DateTime DateTime, DateTime DueDateTime);