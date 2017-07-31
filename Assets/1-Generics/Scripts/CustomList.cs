using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics
{
    public class CustomList<T>
    {
        public T[] list;
        public int amount { get; private set; }

        // Default constructor
        public CustomList() { amount = 0; }
        // gameObjects[0]
        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }
        // Create an Add function
        // Adds an item to the end of the list
        public void Add(T item)
        {
            // Create a new array of amount + 1
            T[] cache = new T[amount + 1];
            // Check if the list has been initialized
            if (list != null)
            {
                // Copy all existing items to the new array
                for (int i = 0; i < list.Length; i++)
                {
                    cache[i] = list[i];
                }
            }
            // Place new item at end of index
            cache[amount] = item;
            // Replace old array with new array
            list = cache;
            // Increment Amount
            amount++;
        }
        public void Clear()
        {
            //Create a new null array
            T[] cache = null;
            //make the current list equal to the new null array
            list = cache;
            //make sure our amount is equal to zero
            amount = 0;
        }

        public void AddRange(T[] items)
        {
            // make a new array of amount plus what we are adding
            T[] cache = new T[amount + items.Length];
            // Check if the list has been initialised
            if(list != null)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    // Copy our current List
                    cache[i] = list[i];
                }
            }
            //Place the new items into the array
            int a = 0;
            for (int i = list.Length; i < cache.Length; i++)
            {
                cache[i] = items[a];
                a++;
            }
            // Replace old array with new array
            list = cache;
            // Increment Amount
            amount+= items.Length;
        }
    }
}