// -----------------------------------------------------------------------
// <copyright file="MarkovChains.cs">
// Copyright (c) Sangik Park. All rights reserved.
// </copyright>
// <author>Sangik Park</author>
// -----------------------------------------------------------------------

namespace ProgrammingChallenge
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    
    /// <summary>
    /// A MarkovChain class which inherits from the below data structure.
    /// Dictionary(T, --> key is the first word and value is the below dictionary.
    ///            Dictionary(T, --> key is the second word.
    ///                       List of T --> value is a list of third words.
    ///                      )
    ///           )
    /// </summary>    
    /// <example>
    /// If input text is "The happy dog followed the car.", chains data will be
    ///     Dictionary( "(START)", Dictionary( "The",   {"happy"} ) )
    ///     Dictionary( "The",     Dictionary( "happy", {"dog"}   ) )
    ///     ...
    ///     Dictionary( "follow",  Dictionary( "the",   {"car"}   ) )
    ///     Dictionary( "the",     Dictionary( "car",   {"."}     ) )
    /// </example>
    /// <typeparam name="T">The type of the Markov chains</typeparam>
    public class MarkovChains<T> : Dictionary<T, Dictionary<T, List<T>>>
    {
        /// <summary>
        /// Adds new data entry to the dictionary.
        /// </summary>
        /// <param name="firstWord">The fist word</param>
        /// <param name="secondWord">The second word</param>
        /// <param name="thirdWord">The third word</param>
        public void Add(T firstWord, T secondWord, T thirdWord)
        {
            if (firstWord == null)
            {
                throw new ArgumentNullException("firstWord");
            }
            else if (secondWord == null)
            {
                throw new ArgumentNullException("secondWord");
            }
            else if (thirdWord == null)
            {
                throw new ArgumentNullException("thirdWord");
            }

            List<T> list = null;
            Dictionary<T, List<T>> dictionary = null;

            try
            {
                if (!this.ContainsKey(firstWord))
                {
                    /* Add a new key for outer dictionary */

                    list = new List<T>();
                    list.Add(thirdWord);

                    dictionary = new Dictionary<T, List<T>>();
                    dictionary.Add(secondWord, list);

                    this.Add(firstWord, dictionary);
                }
                else
                {
                    /* Update existing key for outer dictionary */

                    dictionary = this[firstWord];
                    Debug.Assert(dictionary != null);

                    if (!dictionary.ContainsKey(secondWord))
                    {
                        list = new List<T>();
                        list.Add(thirdWord);

                        dictionary.Add(secondWord, list);
                    }
                    else
                    {
                        list = dictionary[secondWord];
                        Debug.Assert(list != null);
                        list.Add(thirdWord);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}