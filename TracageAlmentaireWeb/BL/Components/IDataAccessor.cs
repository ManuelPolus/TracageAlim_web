using System;
using System.Collections.Generic;
using Tracage.Models;

namespace TracageAlimentaireXamarin.BL.Components
{
    interface IDataAccessor
    {
        bool Save(Object o);

        IEnumerable<T> GetAsList<T>();

        Object GetByIdentifier<T>(T identifier);

        bool DeleteByIdentifier<T>(T identifier);

        void DefineType(object o);
    }
}
