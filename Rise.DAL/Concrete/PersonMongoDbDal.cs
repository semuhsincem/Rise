using Microsoft.Extensions.Options;
using Rise.Core.Mongo;
using Rise.Entity.Concrete;
using Rise.Helper;

namespace Rise.DAL.Concrete
{
    public class PersonMongoDbDal : MongoDbRepositoryBase<Person>, IPersonDal
    {
        public PersonMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
