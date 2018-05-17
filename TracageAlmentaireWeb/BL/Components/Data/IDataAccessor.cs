using System;
using System.Collections.Generic;

namespace TracageAlimentaireXamarin.BL.Components
{
    interface IDataAccessor<T>
    {
        bool Save(Object o);

        IEnumerable<T> GetAsList();

        Object GetByIdentifier(T identifier);

        bool DeleteByIdentifier(T identifier);

        void DefineType(object o);
    }
}
