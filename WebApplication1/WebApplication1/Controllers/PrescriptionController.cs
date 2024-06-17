using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionController(IPrescriptionService service)
    {
        _service = _service;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(PrescriptionDTO prescription)
    {
        var pr = await _service.CreatePrescription(prescription);
        if (pr == null)
            return BadRequest();
        return Created();
    }
}