using Sample.Data;
using Sample.DTO;

namespace Sample.Service
{
    public interface IBackendService
    {
        Task<PersonDTO> AddPersonAsync(PersonDTO person);
        Task<IEnumerable<PersonDTO>> GetAllPersonsAsync();
        Task<PersonDTO> UpdatePersonAsync(int? id, PersonDTO updatedPersonDTO);
        Task<bool> DeletePersonAsync(int? id);
    }
}
