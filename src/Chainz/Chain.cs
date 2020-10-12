using System;
using System.Collections.Generic;
using System.Linq;
using Chainz.Utils;

namespace Chainz
{
    public class Chain<TArg>
    {
        private readonly List<Type> _links = new List<Type>();

        public Chain<TArg> Use<TChainLink>() where TChainLink : IChainLink<TArg>
        {
            _links.Add(typeof(TChainLink));
            return this;
        }

        public Chain<TArg> Prepend<TChainLink>() where TChainLink : IChainLink<TArg>
        {
            _links.Prepend(typeof(TChainLink));
            return this;
        }

        public Chain<TArg> After<TPreviousChainLink, TChainLinkToInsert>()
            where TPreviousChainLink : IChainLink<TArg>
            where TChainLinkToInsert : IChainLink<TArg>
        {
            var index = _links.IndexOf(typeof(TPreviousChainLink)) + 1;
            _links.Insert(index, typeof(TChainLinkToInsert));
            return this;
        }

        public Chain<TArg> Before<TNextChainLink, TChainLinkToInsert>()
            where TNextChainLink : IChainLink<TArg>
            where TChainLinkToInsert : IChainLink<TArg>
        {
            var index = _links.IndexOf(typeof(TNextChainLink));
            _links.Insert(index, typeof(TChainLinkToInsert));
            return this;
        }

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