using System.Collections.Generic;
using System.Linq;
using System;

namespace Extensions
{
    /// <summary>
    /// Represents extensions for collections.
    /// </summary>
    public static class CollectionsExtensions
    {
        private static Random random = new Random();

        /// <summary>
        /// Returns a random index of an item in the list.
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="list">List</param>
        /// <returns>Random item index, -1 if the collection is empty</returns>
        public static int GetRandomIndex<T>(this List<T> list)
        {
            if (list.Count != 0)
            {
                return random.Next(0, list.Count);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Returns a random value of an item in the list.
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="list">List</param>
        /// <returns>Random item value</returns>
        public static T GetRandomValue<T>(this List<T> list)
        {
            if (list.Count != 0)
            {
                return list[list.GetRandomIndex()];
            }
            else
            {
                throw new ArgumentException("Unable to get value collection is empty.");
            }
        }

        /// <summary>
        /// Returns a random index of an item in the array.
        /// </summary>
        /// <typeparam name="T">Array type</typeparam>
        /// <param name="array">Array</param>
        /// <returns>Random item index, -1 if the collection is empty</returns>
        public static int GetRandomIndex<T>(this T[] array)
        {
            if (array.Length != 0)
            {
                return random.Next(0, array.Length);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Returns a random value of an item in the array.
        /// </summary>
        /// <typeparam name="T">Array type</typeparam>
        /// <param name="array">Array</param>
        /// <returns>Random item value</returns>
        public static T GetRandomValue<T>(this T[] array)
        {
            if (array.Length != 0)
            {
                return array[array.GetRandomIndex()];
            }
            else
            {
                throw new ArgumentException("Unable to get value collection is empty.");
            }
        }

        /// <summary>
        /// Returns a random index of an item in the collection.
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="collection">Collection</param>
        /// <returns>Random item index, -1 if the collection is empty</returns>
        public static int GetRandomIndex<T>(this IEnumerable<T> collection)
        {
            if (collection.Count() != 0)
            {
                return random.Next(0, collection.Count());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Returns a random value of an item in the collection.
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="collection">Collection</param>
        /// <returns>Random item value</returns>
        public static T GetRandomValue<T>(this IEnumerable<T> collection)
        {
            if (collection.Count() != 0)
            {
                return collection.ElementAt(collection.GetRandomIndex());
            }
            else
            {
                throw new ArgumentException("Unable to get value collection is empty.");
            }
        }

        /// <summary>
        /// Performs the specified action for all list items.
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <param name="list">List</param>
        /// <param name="action">The Action delegate to perform on each element of the list</param>
        /// <returns>Original list after performing the action for all elements</returns>
        public static List<T> ForEach<T>(this List<T> list, Action<T> action)
        {
            foreach(T item in list)
            {
                action.Invoke(item);
            }
            return list;
        }

        /// <summary>
        /// Performs the specified action for all array items.
        /// </summary>
        /// <typeparam name="T">Array type</typeparam>
        /// <param name="array">Array</param>
        /// <param name="action">The Action delegate to perform on each element of the list</param>
        /// <returns>Original array after performing the action for all elements</returns>
        public static T[] ForEach<T>(this T[] array, Action<T> action)
        {
            foreach(T item in array)
            {
                action.Invoke(item);
            }
            return array;
        }

        /// <summary>
        /// Performs the specified action for all collection items.
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="collection">Collection</param>
        /// <param name="action">The Action delegate to perform on each element of the list</param>
        /// <returns>Original array after performing the action for all elements</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach(T item in collection)
            {
                action.Invoke(item);
            }
            return collection;
        }
    }
}
