using Rise.Entity.Concrete;
using Rise.ViewModels;
using Rise.ViewModels.ServiceResults;
using System.Threading.Tasks;

namespace Rise.BusinessLayer.Abstract
{
    public interface IPersonService
    {
        Task<ServiceResult<Person>> CreatePerson(Person person);
        Task<ServiceResult<Person>> RemovePerson(string id);
        Task<ServiceResult<PersonDetails>> AddInfo(PersonDetails personDetails);
        Task<ServiceResult<PersonDetails>> RemoveInfo(string infoId);
        Task<ServiceResult<PersonDetailsViewModel>> GetAllDetailsByPersonId(string personId);
    }
}
