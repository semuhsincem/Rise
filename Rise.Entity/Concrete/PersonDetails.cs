using EnumHelper;
using Rise.Core;
using Rise.Core.Mongo;

namespace Rise.Entity.Concrete
{
    public class PersonDetails : MongoDbEntity
    {
        public EContactType ContactType { get; set; }
        public string ContactInfo { get; set; }
        public string PersonId { get; set; }
    }
}
