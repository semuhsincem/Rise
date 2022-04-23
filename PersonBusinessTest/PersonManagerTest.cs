using DeepEqual.Syntax;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Rise.BusinessLayer.Abstract;
using Rise.BusinessLayer.Concrete;
using Rise.Entity.Concrete;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PersonBusinessTest
{
    public class PersonManagerTest
    {
        private readonly IPersonService _personService;
        public PersonManagerTest(IPersonService personService)
        {
            _personService = personService;
        }
        [Fact]
        public async Task Create_Person_Test()
        {
            var person = new Person()
            {
                Company = "Test Company",
                Name = "Test User Name",
                Surname = "Test User Last Name"
            };
            var result = await _personService.CreatePerson(person);
            var testResult = (object)result.Data.IsDeepEqual(new Person()
            {
                Id = result.Data?.Id,
                Company = "Test Company",
                Name = "Test User Name",
                Surname = "Test User Last Name"
            });
            Assert.True(Convert.ToBoolean(testResult));
        }
        [Fact]
        public async Task Add_Info_To_Person_Test()
        {
            var person = new Person()
            {
                Company = "Update Person Test Company",
                Name = "Update Person Test User Name",
                Surname = "Update Person Test User Last Name"
            };
            var addedPerson = await _personService.CreatePerson(person);
            if (addedPerson.IsSuccess)
            {
                //Update User
                var addInfoUser = await _personService.AddInfo(new PersonDetails()
                {
                    ContactInfo = "11111",
                    ContactType = EnumHelper.EContactType.Phone,
                    PersonId = addedPerson.Data?.Id
                });

                if (addInfoUser.IsSuccess)
                {
                    var testResult = (object)addInfoUser.Data.IsDeepEqual(new PersonDetails()
                    {
                        Id = addInfoUser.Data?.Id,
                        ContactInfo = "11111",
                        ContactType = EnumHelper.EContactType.Phone,
                        PersonId = addedPerson.Data?.Id
                    });
                    if (Convert.ToBoolean(testResult))
                        Assert.True(Convert.ToBoolean(testResult));
                    else
                        Assert.True(false);

                }
                else
                    Assert.True(false, "An error occured while add info to person...");

            }
            else
                Assert.True(false, "An error occured while add person...");
        }
        [Fact]
        public async Task Remove_Person_Test()
        {
            var person = new Person()
            {
                Company = "Remove Person Test Company",
                Name = "Remove Person Test User Name",
                Surname = "Remove Person Test User Last Name"
            };
            var addedPerson = await _personService.CreatePerson(person);
            if (addedPerson.IsSuccess)
            {
                var removePerson = await _personService.RemovePerson(person.Id);
                Assert.True(removePerson.IsSuccess);
            }
            else
                Assert.True(false, "An error occured while add person...");
        }
    }
}