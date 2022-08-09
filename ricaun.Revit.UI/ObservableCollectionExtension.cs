using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ricaun.Revit.UI
{
    /// <summary>
    /// ObservableCollectionExtension
    /// </summary>
    internal static class ObservableCollectionExtension
    {
        /// <summary>
        /// Sort <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparison"></param>
        internal static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);

            collection.Move(sortableList);
        }

        /// <summary>
        /// OrderBy <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="keySelector"></param>
        internal static void OrderBy<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
        {
            var sortableList = new List<TSource>(collection);
            sortableList = sortableList.OrderBy(keySelector)
                .ToList();

            collection.Move(sortableList);
        }

        /// <summary>
        /// OrderByDescending <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="keySelector"></param>
        internal static void OrderByDescending<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
        {
            var sortableList = new List<TSource>(collection);
            sortableList = sortableList.OrderByDescending(keySelector)
                .ToList();

            collection.Move(sortableList);
        }

        /// <summary>
        /// Move <paramref name="collection"/> based on <paramref name="sortableList"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="sortableList"></param>
        private static void Move<T>(this ObservableCollection<T> collection, List<T> sortableList)
        {
            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
    }
}