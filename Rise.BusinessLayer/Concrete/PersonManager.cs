using EnumHelper;
using MongoDB.Bson;
using Rise.BusinessLayer.Abstract;
using Rise.DAL;
using Rise.DAL.Abstract;
using Rise.Entity.Concrete;
using Rise.Helper.ErrorMessages;
using Rise.ViewModels;
using Rise.ViewModels.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ServiceResult<PersonDetails>> AddInfo(PersonDetails personDetails)
        {
            try
            {
                var person = await _personDal.GetAsync(x => x.Id == personDetails.PersonId);
                if (person != null)
                {
                    personDetails.Id = ObjectId.GenerateNewId().ToString();
                    var data = await _personDetailsDal.AddAsync(personDetails);
                    return new ServiceResult<PersonDetails>(data, ServiceMessages.Success);
                }
                return new ServiceResult<PersonDetails>(null, ServiceMessages.PersonNotFound, false);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<PersonDetails>(null, ServiceMessages.AnErrorOccured, false);
            }
        }

        public async Task<ServiceResult<Person>> CreatePerson(Person person)
        {
            try
            {
                if (person != null)
                {
                    person.Id = ObjectId.GenerateNewId().ToString();
                    var res = await _personDal.AddAsync(person);
                    return new ServiceResult<Person>(res, ServiceMessages.Success);
                }
                return new ServiceResult<Person>(null, ServiceMessages.FillCorrectlyThePersonObject, false);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<Person>(null, ServiceMessages.AnErrorOccured, false);
            }
        }

        public async Task<ServiceResult<PersonDetailsViewModel>> GetAllDetailsByPersonId(string personId)
        {
            try
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
                        model.PersonDetailsInfoViewModel.Add(new PersonDetailsInfoViewModel(x.ContactType, x.ContactInfo, x.Id));
                    });
                    return new ServiceResult<PersonDetailsViewModel>(model, ServiceMessages.Success);
                }
                return new ServiceResult<PersonDetailsViewModel>(null, ServiceMessages.PersonNotFound, false);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<PersonDetailsViewModel>(null, ServiceMessages.AnErrorOccured, false);

            }
        }

        public async Task<ServiceResult<ExcelReportViewModel>> GetPersonCountWithLocation(string location)
        {
            try
            {
                ExcelReportViewModel excelReportViewModel = new ExcelReportViewModel(location);
                //
                var _phoneCount = 0;
                var personList = await _personDetailsDal.GetAll(x => x.ContactType == EContactType.Location && x.ContactInfo == location);
                var personCount = personList.Count;

                excelReportViewModel.PersonCount = personCount;
                var allPerson = await _personDal.GetAll();
                foreach (var item in allPerson)
                {
                    var personDetails = await _personDetailsDal.GetAll(x => x.PersonId == item.Id);
                    if (personDetails.Any(x=>x.ContactInfo == location))
                    {
                        _phoneCount += personDetails.Where(x => x.ContactType == EContactType.Phone).Select(x => x.ContactInfo).Distinct().Count();
                    }
                }
                excelReportViewModel.PhoneCount = _phoneCount;

                return new ServiceResult<ExcelReportViewModel>(excelReportViewModel, ServiceMessages.Success);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<ExcelReportViewModel>(null, ServiceMessages.AnErrorOccured, false);
            }
        }

        public async Task<ServiceResult<PersonDetails>> RemoveInfo(string infoId)
        {
            try
            {
                var isAvailableData = await _personDetailsDal.GetAsync(x => x.Id == infoId);
                if (isAvailableData != null)
                {
                    var data = await _personDetailsDal.DeleteAsync(infoId.ToString());
                    return new ServiceResult<PersonDetails>(data, ServiceMessages.Success);
                }
                return new ServiceResult<PersonDetails>(isAvailableData, ServiceMessages.PersonDetailNotFound, false);
                
            }
            catch (Exception)
            {
                //logger.Error(ex);
                return new ServiceResult<PersonDetails>(null, ServiceMessages.AnErrorOccured, false);
            }
        }

        public async Task<ServiceResult<Person>> RemovePerson(string id)
        {
            try
            {
                var data = await _personDal.DeleteAsync(id.ToString());
                return new ServiceResult<Person>(data, ServiceMessages.Success);
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                return new ServiceResult<Person>(null, ServiceMessages.AnErrorOccured, false);
            }
        }
    }
}
