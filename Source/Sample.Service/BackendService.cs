using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.DTO;

namespace Sample.Service
{
    public class BackendService : IBackendService
    {
        private readonly PersonsContext _context;

        public BackendService(PersonsContext context)
        {

            _context = context;
        }

        public async Task<PersonDTO> AddPersonAsync(Person person)
        {
            var personDTO = new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };

            _context.Persons.Add(person);

            await _context.SaveChangesAsync();

            return personDTO;
        }

        public async Task<bool> DeletePersonAsync(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException($"Person with ID {id} could not be found.");
            }

            var person = await _context.Persons.FindAsync(id);

            if (person is null)
            {
                return false;
            }

            _context.Persons.Remove(person);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllPersonsAsync()
        {
            var personsList = await _context.Persons.ToListAsync();

            return personsList.Select(person => MapPersonToDTO(person));
        }

        public async Task<PersonDTO> UpdatePersonAsync(int? id, PersonDTO updatedPersonDTO)
        {
            if (id is null)
            {
                throw new ArgumentNullException($"Person with ID {id} could not be found.");
            }

            var personToBeUpdated = await _context.Persons.FindAsync(id);

            var dtoPerson = MapPersonToDTO(personToBeUpdated);

            dtoPerson.Id = updatedPersonDTO.Id;
            dtoPerson.FirstName = updatedPersonDTO.FirstName;
            dtoPerson.LastName = updatedPersonDTO.LastName;

            await _context.SaveChangesAsync();

            return dtoPerson;
        }

        private PersonDTO MapPersonToDTO(Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }
    }
}
