using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Galaga.Extensions
{
    /// <summary>
    ///     The collection extensions class
    /// </summary>
    public static class CollectionExtensions
    {
        #region Methods

        /// <summary>
        ///     Makes your collection observable
        /// </summary>
        /// <typeparam name="T">The type of element in your collection</typeparam>
        /// <param name="collection">the collection of elements</param>
        /// <returns>an observable collection of type T</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

        #endregion
    }
}