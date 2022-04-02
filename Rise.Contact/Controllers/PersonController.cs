using Microsoft.AspNetCore.Mvc;
using Rise.BusinessLayer.Abstract;
using Rise.Entity.Concrete;
using Rise.ViewModels;
using System.Threading.Tasks;

namespace Rise.Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            this._personService = personService;
        }
        [HttpGet]
        [Route("GetAllDetailsByPersonId/{personId}")]
        public async Task<PersonDetailsViewModel> GetAllDetailsByPersonId(string personId)
        {
            return await _personService.GetAllDetailsByPersonId(personId);
        }

        [HttpGet]
        [Route("RemovePerson/{personId}")]
        public async Task<Person> RemovePerson(string personId)
        {
            return await _personService.RemovePerson(personId);
        }

        [HttpPost]
        [Route("AddInfo")]
        public async Task<PersonDetails> AddInfo([FromBody] PersonDetails personDetails)
        {
            return await _personService.AddInfo(personDetails);
        }

        [HttpPost]
        [Route("CreatePerson")]
        public async Task<Person> CreatePerson([FromBody] Person person)
        {
            return await _personService.CreatePerson(person);
        }

        [HttpPost]
        [Route("RemoveInfo/{infoId}")]
        public async Task<PersonDetails> RemoveInfo(string infoId)
        {
            return await _personService.RemoveInfo(infoId);
        }
    }
}
