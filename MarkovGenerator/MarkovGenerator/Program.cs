//-----------------------------------------------------------------------
// <copyright file="Program.cs">
// Copyright (c) Sangik Park. All rights reserved.
// </copyright>
// <author>Sangik Park</author>
//-----------------------------------------------------------------------

namespace ProgrammingChallenge
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    /// <summary>
    /// The entry point class.
    /// </summary>    
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Command-line arguments. args[0] is a source text filename.</param>
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please enter a source text filename.");
                Console.WriteLine("Usage: MarkovGenerator source");
                return;
            }

            string sourceFileName = args[0];

            if (!File.Exists(sourceFileName))
            {
                Console.WriteLine("The source text file does not exist.");
                return;
            }

            string inputText = null;

            try
            {
                // REVIEW: Use StreamReader.ReadLine() for large files.
                inputText = File.ReadAllText(sourceFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            MarkovRandomTextGenerator textGenerator = null;
            string randomOutputText = null;

            try
            {
                textGenerator = new MarkovRandomTextGenerator(inputText);
                randomOutputText = textGenerator.GenerateRandomText();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            Console.WriteLine(randomOutputText);
        }
    }
}