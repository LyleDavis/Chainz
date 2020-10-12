using System;
using System.Collections.Generic;

namespace Chainz.Dummy
{
    class Program
    {
        static void Main(string[] args)
        {
            var chain = new Chain<Param>()
                .Use<SecondChainLink>()
                .Prepend<FirstChainLink>()
                .After<SecondChainLink, ThirdChainLink>()
                .Use<FifthChainLink>()
                .Before<FifthChainLink, FourthChainLink>()
                .Use<ChainLinkToReplace>()
                .InsertAt<SixthChainLink>(5)
                .Replace<ChainLinkToReplace, SeventhChainLink>()
                .Compile();
            var p = new Param { PropOne = "PropOne" };
            
            chain.Handle(p);
            p.ListOfLinks.ForEach(Console.WriteLine);
        }
        
        class Param
        {
            public string PropOne { get; set; }
            public List<string> ListOfLinks { get; set; } = new List<string>();
        }

        class FirstChainLink : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                args.ListOfLinks.Add("First");
                Next.Handle(args);
            }
        }
        
        class SecondChainLink : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                args.ListOfLinks.Add("Second");
                Next.Handle(args);
            }
        }

        class ThirdChainLink : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                args.ListOfLinks.Add("Third");
                Next.Handle(args);
            }
        }

        class FourthChainLink : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                args.ListOfLinks.Add("Fourth");
                Next.Handle(args);
            }
        }

        class FifthChainLink : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                args.ListOfLinks.Add("Fifth");
                Next.Handle(args);
            }
        }

        class SixthChainLink : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                args.ListOfLinks.Add("Sixth");
                Next.Handle(args);
            }
        }

        class SeventhChainLink : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                args.ListOfLinks.Add("Seventh");
                Next.Handle(args);
            }
        }

        class ChainLinkToReplace : IChainLink<Param>
        {
            public IChainLink<Param> Next { get; set; }
            public void Handle(Param args)
            {
                throw new NotImplementedException();
            }
        }
    }
}