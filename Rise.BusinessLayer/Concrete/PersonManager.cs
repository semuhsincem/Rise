using Rise.BusinessLayer.Abstract;
using Rise.DAL;
using Rise.DAL.Abstract;
using Rise.Entity.Concrete;
using Rise.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.BusinessLayer.Concrete
{
    public class PersonManager : IPersonService
    {
        private readonly IPersonDal _personDal;
        private readonly IPersonDetailsDal _personDetailsDal;
        public PersonManager(IPersonDal personDal, IPersonDetailsDal personDetailsDal)
        {
            _personDal = personDal;
            _personDetailsDal = personDetailsDal;
        }
        public async Task<PersonDetails> AddInfo(PersonDetails personDetails)
        {
            var person = await _personDal.GetAsync(x => x.Id == personDetails.PersonId);
            if (person != null)
            {
                return await _personDetailsDal.AddAsync(personDetails);
            }
            return null;
        }

        public async Task<Person> CreatePerson(Person person)
        {
            if (person != null)
            {
                var res = await _personDal.AddAsync(person);
                return res;
            }
            return null;
        }

        public async Task<PersonDetailsViewModel> GetAllDetailsByPersonId(string personId)
        {
            var person = await _personDal.GetAsync(x => x.Id == personId);
            PersonDetailsViewModel model;
            if (person != null)
            {
                model = new PersonDetailsViewModel(person.Id, person.Name, person.Surname, person.Company);
                //Get Person Details

                var personDetails = await _personDetailsDal.GetAll(x => x.PersonId == personId);
                personDetails.ForEach(x =>
                {
                    model.PersonDetailsInfoViewModel.Add(new PersonDetailsInfoViewModel() { eContactType = x.ContactType, Info = x.ContactInfo, PersonDetailsId = x.Id });
                });
                return model;
            }
            return new PersonDetailsViewModel();
        }

        public async Task<PersonDetails> RemoveInfo(string infoId)
        {
            return await _personDetailsDal.DeleteAsync(infoId.ToString());
        }

        public async Task<Person> RemovePerson(string id)
        {
            return await _personDal.DeleteAsync(id.ToString());
        }
    }
}
