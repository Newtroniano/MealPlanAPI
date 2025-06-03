using AutoMapper;
using MealPlanAPI.Models.DTOs.PatientDto;
using Microsoft.EntityFrameworkCore;

public interface IPatientService
{
    Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto);
    Task<PatientDto?> GetPatientByIdAsync(int id);
    Task<List<PatientDto>> GetAllPatientsAsync(string? nameFilter, int page, int pageSize);
    Task DeletePatientAsync(int id);
    Task ReactivatePatientAsync(int id);
    Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientDto patientDto);
}

public class PatientService : IPatientService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public PatientService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto)
    {
        var patient = _mapper.Map<Patient>(patientDto);
        _dbContext.Patients.Add(patient);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<PatientDto?> GetPatientByIdAsync(int id)
    {
        var patient = await _dbContext.Patients.FindAsync(id);
        return patient == null || patient.IsDeleted ? null : _mapper.Map<PatientDto>(patient);
    }

    public async Task<List<PatientDto>> GetAllPatientsAsync(string? nameFilter, int page, int pageSize)
    {
        var query = _dbContext.Patients.Where(p => !p.IsDeleted);

        if (!string.IsNullOrEmpty(nameFilter))
            query = query.Where(p => p.Name.Contains(nameFilter));

        var patients = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<List<PatientDto>>(patients);
    }

    public async Task DeletePatientAsync(int id)
    {
        var patient = await _dbContext.Patients.FindAsync(id);
        if (patient == null || patient.IsDeleted)
            throw new KeyNotFoundException("Paciente não encontrado");

        patient.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task ReactivatePatientAsync(int id)
    {
        var patient = await _dbContext.Patients.FindAsync(id);
        if (patient == null)
            throw new KeyNotFoundException("Paciente não encontrado");

        if (!patient.IsDeleted)
            throw new InvalidOperationException("Paciente já está ativo");

        patient.IsDeleted = false;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientDto patientDto)
    {
        var patient = await _dbContext.Patients.FindAsync(id);
        if (patient == null || patient.IsDeleted)
            throw new KeyNotFoundException("Paciente não encontrado");

        _mapper.Map(patientDto, patient);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<PatientDto>(patient);
    }
}