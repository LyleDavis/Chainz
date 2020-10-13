namespace Chainz.Tests.Shared
{
    internal class FirstChainLink : IChainLink<ChainParam>
    {
        public IChainLink<ChainParam> Next { get; set; }

        public void Handle(ChainParam args)
        {
            args.Values.Add(1);
            Next.Handle(args);
        }
    }

    internal class SecondChainLink : IChainLink<ChainParam>
    {
        public IChainLink<ChainParam> Next { get; set; }

        public void Handle(ChainParam args)
        {
            args.Values.Add(2);
            Next.Handle(args);
        }
    }

    internal class ThirdChainLink : IChainLink<ChainParam>
    {
        public IChainLink<ChainParam> Next { get; set; }

        public void Handle(ChainParam args)
        {
            args.Values.Add(3);
            Next.Handle(args);
        }
    }

    internal class FourthChainLink : IChainLink<ChainParam>
    {
        public IChainLink<ChainParam> Next { get; set; }

        public void Handle(ChainParam args)
        {
            args.Values.Add(4);
            Next.Handle(args);
        }
    }

    internal class FifthChainLink : IChainLink<ChainParam>
    {
        public IChainLink<ChainParam> Next { get; set; }

        public void Handle(ChainParam args)
        {
            args.Values.Add(5);
            Next.Handle(args);
        }
    }

    internal class SixthChainLink : IChainLink<ChainParam>
    {
        public IChainLink<ChainParam> Next { get; set; }

        public void Handle(ChainParam args)
        {
            args.Values.Add(6);
            Next.Handle(args);
        }
    }

    internal class DoesNotContinueChainLink : IChainLink<ChainParam>
    {
        public IChainLink<ChainParam> Next { get; set; }

        public void Handle(ChainParam args)
        {
#pragma warning disable 162
            // ReSharper disable once HeuristicUnreachableCode
            if (false) Next.Handle(args);
#pragma warning restore 162
        }
    }
}