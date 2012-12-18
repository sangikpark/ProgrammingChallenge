// -----------------------------------------------------------------------
// <copyright file="MarkovRandomTextGenerator.cs">
// Copyright (c) Sangik Park. All rights reserved.
// </copyright>
// <author>Sangik Park</author>
// -----------------------------------------------------------------------

namespace ProgrammingChallenge
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A MarkovRandomTextGenerator class
    /// </summary>    
    public class MarkovRandomTextGenerator : IMarkovRandomGenerator<string>
    {
        /// <summary>
        /// Constant for the starting word.
        /// </summary>
        private const string StartingWord = "(START)";

        /// <summary>
        /// Initializes a new instance of the MarkovRandomTextGenerator class.
        /// </summary>
        public MarkovRandomTextGenerator()
        {
            this.Chains = new MarkovChains<string>(); 
        }

        /// <summary>
        /// Initializes a new instance of the MarkovRandomTextGenerator class.
        /// </summary>
        /// <param name="inputText">A string containing all lines of the file.</param>
        public MarkovRandomTextGenerator(string inputText)
        {
            this.Chains = new MarkovChains<string>();
            this.CreateChains(inputText);
        }

        /// <summary>
        /// Gets or sets for Markov chains.
        /// </summary>
        public MarkovChains<string> Chains
        {
            get;
            set;
        }

        /// <summary>
        /// Creates Markov chains from the input text string.
        /// </summary>
        /// <param name="inputText">A string containing all lines of the file</param>
        public void CreateChains(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                throw new ArgumentException("InputText is null or empty.", "inputText");
            }

            string[] tokens = Tokenize(inputText);
            if (tokens == null || tokens.Length == 0)
            {
                return; // No token!
            }

            bool isStartingWord = true;
            string firstWord = null, secondWord = null, thirdWord = null;

            foreach (string token in tokens)
            {
                if (string.IsNullOrEmpty(token))
                {
                    continue; // Skip.
                }

                if (isStartingWord)
                {
                    firstWord = StartingWord;
                    secondWord = token;
                    isStartingWord = false;
                    continue;
                }

                thirdWord = token;

                try
                {
                    if (!this.Chains.ContainsKey(firstWord))
                    {
                        this.Chains.Add(firstWord, secondWord, thirdWord);
                    }
                    else
                    {
                        this.Chains.Update(firstWord, secondWord, thirdWord);
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                if (IsEndingSentence(thirdWord))
                {
                    firstWord = secondWord = null;
                    isStartingWord = true;
                }
                else
                {
                    firstWord = secondWord;
                    Debug.Assert(!string.IsNullOrEmpty(firstWord));

                    secondWord = thirdWord;
                    Debug.Assert(!string.IsNullOrEmpty(secondWord));
                }
            }
        }

        /// <summary>
        /// Generates a random text string.
        /// </summary>
        /// <returns>A random generated text string</returns>
        public string GenerateRandomText()
        {
            if (this.Chains == null)
            {
                throw new InvalidOperationException("Markov chains are null.");
            }

            string outputText = null;
            string firstWord = StartingWord, secondWord = null, thirdWord = null;

            bool isStartingWord = true;

            Dictionary<string, List<string>> dictionary = null;
            KeyValuePair<string, List<string>> kvp;
            List<string> list = null;

            Random random = new Random();

            while (true)
            {
                try
                {
                    // Find inner dictionary data with the first word.
                    dictionary = this.Chains[firstWord];

                    if (isStartingWord)
                    {
                        // Random select for the second word.
                        kvp = dictionary.ElementAt(random.Next(dictionary.Count));
                        secondWord = kvp.Key;
                        Debug.Assert(!string.IsNullOrEmpty(secondWord));

                        // Random select for the third word.
                        list = kvp.Value as List<string>;
                        thirdWord = list.ElementAt(random.Next(list.Count));
                        Debug.Assert(!string.IsNullOrEmpty(thirdWord));

                        outputText += secondWord;

                        isStartingWord = false;
                    }
                    else
                    {
                        // Random select for the third word.
                        list = dictionary[secondWord];
                        thirdWord = list.ElementAt(random.Next(list.Count));
                        Debug.Assert(!string.IsNullOrEmpty(thirdWord));
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                outputText += " " + thirdWord;

                if (IsEndingSentence(thirdWord))
                {
                    break;
                }

                firstWord = secondWord;
                secondWord = thirdWord;
            }

            // Removes white spaces from some punctuations such as sentence enders.
            outputText = outputText.Replace(" .", ".").Replace(" ?", "?").Replace(" !", "!");
            outputText = outputText.Replace(" ,", ",").Replace(" ;", ";").Replace(" :", ":");

            return outputText;
        }

        /// <summary>
        /// Tokenizes input text string.
        /// 1. Removes non alphabetic letters except some punctuations such as sentence enders.
        /// 2. Adds additional white space between words and punctuations to recognize the punctuations as tokens.
        ///    - Treat periods (.), question marks (?), and exclamation marks (!) as the only sentence enders.
        ///    - Comma (,), semi-colon (;), and colon (:) should be treated like a word.
        /// 3. Splits input text string with white space and new line delimiters.
        /// </summary>
        /// <param name="inputText">A string containing all lines of the file.</param>
        /// <returns>An array of tokenized strings</returns>
        private static string[] Tokenize(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return null;
            }

            inputText = Regex.Replace(inputText, "[^a-zA-Z.?!\\-',;:\n ]", string.Empty);

            inputText = inputText.Replace(".", " .").Replace("?", " ?").Replace("!", " !");            
            inputText = inputText.Replace(",", " ,").Replace(";", " ;").Replace(":", " :");

            return inputText.Split(' ', '\n');
        }

        /// <summary>
        /// Checks if an input word string is whether ending sentence or not.
        /// </summary>
        /// <param name="word">A word string</param>
        /// <returns>True if an input word is sentence ender. Otherwise, returns false.</returns>
        private static bool IsEndingSentence(string word)
        {
            if (word == "." || word == "!" || word == "?")
            {
                return true;
            }

            return false;
        }
    }
}