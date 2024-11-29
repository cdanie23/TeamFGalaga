using System.Collections.Generic;

namespace Galaga.Extensions
{
    /// <summary>
    ///     The list extensions class
    /// </summary>
    public static class ListExtensions
    {
        #region Methods

        /// <summary>
        ///     Replaces the old element with the new element
        /// </summary>
        /// <typeparam name="T">the type of element</typeparam>
        /// <param name="list">the list of elements</param>
        /// <param name="oldElement">the old element</param>
        /// <param name="newElement">the new element</param>
        /// <returns></returns>
        public static bool Replace<T>(this IList<T> list, T oldElement, T newElement)
        {
            var index = list.IndexOf(oldElement);
            if (index != -1)
            {
                list[index] = newElement;
                return true;
            }

            return false;
        }

        #endregion
    }
}