using Rise.Core.Mongo;

namespace Rise.Entity.Concrete
{
    public class Person : MongoDbEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }
}
