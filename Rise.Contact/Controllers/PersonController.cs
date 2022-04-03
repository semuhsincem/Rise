using Microsoft.AspNetCore.Mvc;
using Rise.BusinessLayer.Abstract;
using Rise.Entity.Concrete;
using Rise.ViewModels;
using Rise.ViewModels.ServiceResults;
using System.Threading.Tasks;

namespace Rise.Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;
        public PersonController(IPersonService personService, IReportService reportService)
        {
            _personService = personService;
            _reportService = reportService;
        }
        [HttpGet]
        [Route("GetAllDetailsByPersonId/{personId}")]
        public async Task<ServiceResult<PersonDetailsViewModel>> GetAllDetailsByPersonId(string personId)
        {
            return await _personService.GetAllDetailsByPersonId(personId);
        }

        [HttpGet]
        [Route("RemovePerson/{personId}")]
        public async Task<ServiceResult<Person>> RemovePerson(string personId)
        {
            return await _personService.RemovePerson(personId);
        }

        [HttpPost]
        [Route("AddInfo")]
        public async Task<ServiceResult<PersonDetails>> AddInfo([FromBody] PersonDetails personDetails)
        {
            return await _personService.AddInfo(personDetails);
        }

        [HttpPost]
        [Route("CreatePerson")]
        public async Task<ServiceResult<Person>> CreatePerson([FromBody] Person person)
        {
            return await _personService.CreatePerson(person);
        }

        [HttpPost]
        [Route("RemoveInfo/{infoId}")]
        public async Task<ServiceResult<PersonDetails>> RemoveInfo(string infoId)
        {
            return await _personService.RemoveInfo(infoId);
        }

        
        //Rehbere Kayıtlı O konumdaki kişi sayısı
        [HttpGet]
        [Route("GetPersonCountWithLocation/{location}")]
        public async Task<ServiceResult<ExcelReportViewModel>> GetPersonCountWithLocation(string location)
        {
            return await _personService.GetPersonCountWithLocation(location);
        }

    }
}
