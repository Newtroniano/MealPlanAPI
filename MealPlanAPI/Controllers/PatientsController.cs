// MealPlanAPI/Controllers/PatientController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MealPlanAPI.Models.DTOs.PatientDto;


[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public PatientController(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreatePatient([FromBody] CreatePatientDto patientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var patient = _mapper.Map<Patient>(patientDto);

        _dbContext.Patients.Add(patient);
        _dbContext.SaveChanges();

        var createdPatientDto = _mapper.Map<PatientDto>(patient);

        return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, createdPatientDto);
    }

    [HttpGet("{id}")]
    public IActionResult GetPatientById(int id)
    {
        var patient = _dbContext.Patients.Find(id);
        if (patient == null)
        {
            return NotFound();
        }

        var patientDto = _mapper.Map<PatientDto>(patient);
        return Ok(patientDto);
    }


    [HttpGet]
    public IActionResult GetAllPatients(
    [FromQuery] string? nameFilter = null,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var query = _dbContext.Patients
            .Where(p => !p.IsDeleted);

        if (!string.IsNullOrEmpty(nameFilter))
        {
            query = query.Where(p => p.Name.Contains(nameFilter));
        }

        var patients = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var patientDtos = _mapper.Map<List<PatientDto>>(patients);

        return Ok(patientDtos);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePatient(int id)
    {
        var patient = _dbContext.Patients.Find(id);

        if (patient == null || patient.IsDeleted)
        {
            return NotFound("Paciente não encontrado.");
        }

        // Soft delete (marca como deletado)
        patient.IsDeleted = true;
        _dbContext.SaveChanges();

        return NoContent(); // Retorna 204 No Content
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePatient(int id, [FromBody] UpdatePatientDto patientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var patient = _dbContext.Patients.Find(id);

        if (patient == null || patient.IsDeleted)
        {
            return NotFound();
        }

        _mapper.Map(patientDto, patient); 
        _dbContext.SaveChanges();

        return Ok(_mapper.Map<PatientDto>(patient));
    }

}