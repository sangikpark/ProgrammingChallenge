// -----------------------------------------------------------------------
// <copyright file="Program.cs">
// Copyright (c) Sangik Park. All rights reserved.
// </copyright>
// <author>Sangik Park</author>
// -----------------------------------------------------------------------

namespace ProgrammingChallenge
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// The entry point class.
    /// </summary>   
    class Program
    {
        /// <summary>
        /// Finds the highest 4 numbers in an unordered array of integers in O(n) time. 
        /// </summary>
        /// <example>
        /// If the unsorted array is { 2, 1, 4, 8, 5, 6, 10, 3, 7, 9 },
        /// this method will return { 10, 9, 8, 7 }.
        /// </example>
        /// <param name="array">The unordered array of integers</param>
        /// <returns>The highest 4 numbers in the array</returns>
        public static int[] GetHighestFourNumbers(int[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (array.Length <= 4)
                return array;

            int[] result = new int[4];

            try
            {
                // Create a sorted list using the unsorted array - MSDN says it's O(n) time.
                // Or we may use a heap data structure instead of the sorted list - still O(n).
                SortedList list = new SortedList(array.ToDictionary(key => key));

                // Find the highest 4 numbers - O(n) time.
                for (int i = 0; i < 4; i++)
                {
                    result[i] = (int)list.GetKey(list.Count - 1 - i);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Command-line arguments</param>
        static void Main(string[] args)
        {
            int[] input = new int[10] { 2, 1, 4, 8, 5, 6, 10, 3, 7, 9 };
            int[] output = new int[4];

            try
            {
                output = GetHighestFourNumbers(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }
    }
}