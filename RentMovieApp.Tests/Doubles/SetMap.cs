using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMovieApp.Tests.Doubles
{
    public class SetMap : KeyedCollection<Type, object>
    {
        // class to map collection like dbcontex
        public HashSet<T> Use<T>(IEnumerable<T> sourceData)
        {
            var set = new HashSet<T>(sourceData);
            if(Contains(typeof(T)))
            {
                Remove(typeof(T));
            }
            Add(set);
            return set;
        }

        public HashSet<T> Get<T>() => (HashSet<T>)this[typeof(T)];

        protected override Type GetKeyForItem(object item)
        {
            return item.GetType().GetGenericArguments().Single();
        }
    }
}
