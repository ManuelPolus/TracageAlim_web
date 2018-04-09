using System;
using System.Collections.Generic;
using Tracage.Models;

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
