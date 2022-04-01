using Microsoft.Extensions.Options;
using Rise.Core.Mongo;
using Rise.DAL.Abstract;
using Rise.Entity.Concrete;
using Rise.Helper;

namespace Rise.DAL.Concrete
{
    public class PersonDetailsMongoDbDal : MongoDbRepositoryBase<PersonDetails>, IPersonDetailsDal
    {
        public PersonDetailsMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
