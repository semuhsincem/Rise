using Rise.Entity.Concrete;
using Rise.ViewModels;
using System.Threading.Tasks;

namespace Rise.BusinessLayer.Abstract
{
    public interface IPersonService
    {
        Task<Person> CreatePerson(Person person);
        Task<Person> RemovePerson(string id);
        Task<PersonDetails> AddInfo(PersonDetails personDetails);
        Task<PersonDetails> RemoveInfo(string infoId);
        Task<PersonDetailsViewModel> GetAllDetailsByPersonId(string personId);
    }
}
