using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.DTO;

namespace Sample.Service;

public class BackendService : IBackendService
{
    private readonly PersonsContext _context;

    public BackendService(PersonsContext context)
    {
        _context = context;
    }

    public async Task<PersonDTO> AddPersonAsync([FromBody] PersonDTO dtoPerson)
    {
        var person = new Person
        {
            Id = dtoPerson.Id,
            FirstName = dtoPerson.FirstName,
            LastName = dtoPerson.LastName,
        };

        _context.Persons.Add(person);

        await _context.SaveChangesAsync();

        return MapPersonToDTO(person);
    }

    public async Task<Result> DeletePersonAsync(int id)
    {
        var person = await _context.Persons.FindAsync(id);

        if (person == null)
            return Result.Error($"Person with ID {id} could not be found.");

        _context.Persons.Remove(person);

        await _context.SaveChangesAsync();

        return Result.Success($"Person with ID {id} deleted successfully.");
    }

    public async Task<IEnumerable<PersonDTO>> GetAllPersonsAsync()
    {
        var personsList = await _context.Persons.ToListAsync();

        return personsList.Select(person => MapPersonToDTO(person));
    }

    public async Task<Result> UpdatePersonAsync(int id, [FromBody] UpdatedPersonDTO updatedPersonDTO)
    {
        var personToBeUpdated = await _context.Persons.FindAsync(id);

        if (personToBeUpdated is null)
            return Result.Error($"Person with ID {id} could not be found.");

        personToBeUpdated.FirstName = updatedPersonDTO.FirstName;
        personToBeUpdated.LastName = updatedPersonDTO.LastName;

        await _context.SaveChangesAsync();

        return Result.Success("Update Successful.");
    }

    private PersonDTO MapPersonToDTO(Person person) => new PersonDTO
    {
        Id = person.Id,
        FirstName = person.FirstName,
        LastName = person.LastName,
    };
}
