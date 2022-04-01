using EnumHelper;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rise.ViewModels
{
    public class PersonDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<PersonDetailsInfoViewModel> PersonDetailsInfoViewModel { get; set; }
        public PersonDetailsViewModel()
        {
            PersonDetailsInfoViewModel = new List<PersonDetailsInfoViewModel>();
        }

        public PersonDetailsViewModel(string id, string name, string surname , string company): this()
        {
            Id = id;
            Name = name;
            Surname = surname;
            Company = company;
        }
    }
    public class PersonDetailsInfoViewModel
    {
        public EContactType  eContactType { get; set; }
        public string Info { get; set; }
        [DisplayName("Infos")]
        public string PersonDetailsId { get; set; }
    }
}
