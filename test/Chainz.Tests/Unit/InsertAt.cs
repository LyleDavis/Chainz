using Chainz.Tests.Shared;
using Xunit;

namespace Chainz.Tests.Unit
{
    public class InsertAt
    {
        [Fact]
        public void AddsChainLinkAtIndex()
        {
            var chain = new Chain<ChainParam>()
                .Use<FirstChainLink>()
                .Use<ThirdChainLink>()
                .InsertAt<SecondChainLink>(1)
                .Compile();
            Assert.IsType<FirstChainLink>(chain);
            Assert.IsType<SecondChainLink>(chain.Next);
            Assert.IsType<ThirdChainLink>(chain.Next.Next);
        }
    }
}