using Rise.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Rise.BusinessLayer.Comparers
{
    class PersonEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            return !(x.Name.ToLowerInvariant() == y.Name.ToLowerInvariant() && x.Surname.ToLowerInvariant() == y.Surname.ToLowerInvariant());
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.GetHashCode();
        }
    }
}
