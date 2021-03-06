﻿// -----------------------------------------------------------------------
// <copyright file="IMarkovRandomGenerator.cs">
// Copyright (c) Sangik Park. All rights reserved.
// </copyright>
// <author>Sangik Park</author>
// -----------------------------------------------------------------------

namespace ProgrammingChallenge
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// IMarkovRandomGenerator interface.
    /// </summary>
    /// <typeparam name="T">The type of the Markov Random Generator</typeparam>
    public interface IMarkovRandomGenerator<T>
    {
        #region Properties
        /// <summary>
        /// Gets or sets for the Markov chains.
        /// </summary>
        MarkovChains<T> Chains
        {
            get;
            set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the Markov chains from the input T data.
        /// </summary>
        /// <param name="inputText">An input T data</param>
        void CreateChains(T inputText);

        /// <summary>
        /// Generates a random T data.
        /// </summary>
        /// <returns>A random generated T data</returns>
        T GenerateRandomText();
        #endregion
    }
}