using Sample.Data;
using Sample.DTO;

namespace Sample.Service;

public interface IBackendService
{
    Task<Result> AddPersonAsync(PersonDTO person);
    Task<IEnumerable<PersonDTO>> GetAllPersonsAsync();
    Task<Result> UpdatePersonAsync(int id, UpdatedPersonDTO updatedPersonDTO);
    Task<Result> DeletePersonAsync(int id);
}
