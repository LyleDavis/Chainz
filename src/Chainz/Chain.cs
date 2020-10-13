using System;
using System.Collections.Generic;
using Chainz.Utils;

namespace Chainz
{
    /// <summary>
    ///     A typed "Middleware" chain, pass a typed object along the chain of handlers.
    ///     0 - n handlers can choose to handle and/or move on to the next handler with Next.Handle(args)
    /// </summary>
    /// <typeparam name="TArg">
    ///     The Type of the chain parameter.
    ///     If you need multiple parameters, wrap them in an object.
    ///     If you need a middleware to be part of multiple chains, abstract accordingly with interfaces.
    /// </typeparam>
    public class Chain<TArg>
    {
        private readonly List<Type> _links = new List<Type>();

        /// <summary>
        ///     Adds a chainlink to the beginning of the chain.
        /// </summary>
        /// <typeparam name="TChainLink">The handler type</typeparam>
        /// <returns>Returns self for fluid interface</returns>
        public Chain<TArg> Use<TChainLink>() where TChainLink : IChainLink<TArg>
        {
            _links.Add(typeof(TChainLink));
            return this;
        }

        /// <summary>
        ///     Adds a chainlink to the end of the chain.
        /// </summary>
        /// <typeparam name="TChainLink">The handler type</typeparam>
        /// <returns>Returns self for fluid interface</returns>
        public Chain<TArg> Prepend<TChainLink>() where TChainLink : IChainLink<TArg>
        {
            return InsertAt<TChainLink>(0);
        }

        /// <summary>
        ///     Adds a chainlink after the specified chainlink.
        /// </summary>
        /// <typeparam name="TPreviousChainLink">The chainlink type to insert after</typeparam>
        /// <typeparam name="TChainLinkToInsert">The chainlink type to insert</typeparam>
        /// <returns>Returns self for fluid interface</returns>
        public Chain<TArg> After<TPreviousChainLink, TChainLinkToInsert>()
            where TPreviousChainLink : IChainLink<TArg>
            where TChainLinkToInsert : IChainLink<TArg>
        {
            var index = _links.IndexOf(typeof(TPreviousChainLink)) + 1;
            return InsertAt<TChainLinkToInsert>(index);
        }

        /// <summary>
        ///     Adds a chainlink before the specified chainlink
        /// </summary>
        /// <typeparam name="TNextChainLink">The chainlink type to insert before</typeparam>
        /// <typeparam name="TChainLinkToInsert">The chainlink type to insert</typeparam>
        /// <returns>Returns self for fluid interface</returns>
        public Chain<TArg> Before<TNextChainLink, TChainLinkToInsert>()
            where TNextChainLink : IChainLink<TArg>
            where TChainLinkToInsert : IChainLink<TArg>
        {
            var index = _links.IndexOf(typeof(TNextChainLink));
            return InsertAt<TChainLinkToInsert>(index);
        }

        /// <summary>
        ///     Adds a chainlink at the specified index (zero-indexed)
        /// </summary>
        /// <param name="index">The index at which to insert the chainlink</param>
        /// <typeparam name="TChainLinkToInsert">The chainlink type to insert</typeparam>
        /// <returns>Returns self for fluid interface</returns>
        public Chain<TArg> InsertAt<TChainLinkToInsert>(int index)
            where TChainLinkToInsert : IChainLink<TArg>
        {
            _links.Insert(index, typeof(TChainLinkToInsert));
            return this;
        }

        /// <summary>
        ///     Replaces a particular chainlink with the specified chainlink
        /// </summary>
        /// <typeparam name="TChainLinkToRemove">The chainlink type to replace</typeparam>
        /// <typeparam name="TChainLinkToAdd">The chainlink type to replace _with_</typeparam>
        /// <returns>Returns self for fluid interface</returns>
        public Chain<TArg> Replace<TChainLinkToRemove, TChainLinkToAdd>()
            where TChainLinkToRemove : IChainLink<TArg>
            where TChainLinkToAdd : IChainLink<TArg>
        {
            _links[_links.IndexOf(typeof(TChainLinkToRemove))] = typeof(TChainLinkToAdd);
            return this;
        }

        /// <summary>
        ///     Compiles the Chain into something that can be run by calling `chain.Handle(args)`
        ///     It is formed like a linked-list, each chain has a pointer (`Next`) to the next handler.
        /// </summary>
        /// <returns>The runnable chain, i.e. the first chainlink</returns>
        public IChainLink<TArg> Compile()
        {
            var first = CreateLinkFromType(_links[0]);
            var current = first;
            for (var i = 1; i < _links.Count; i++)
            {
                var next = CreateLinkFromType(_links[i]);
                current.Next = next;
                current = next;
            }

            // The last middleware in the chain won't have any next middleware
            // so if there's a call to Handle it will result in a NullReferenceException.
            // Overcome this by setting a blank chainlink last.
            current.Next = new BlankChainLink<TArg>();
            return first;
        }

        private IChainLink<TArg> CreateLinkFromType(Type type)
        {
            return Reflector.Instantiate<IChainLink<TArg>>(type);
        }
    }
}