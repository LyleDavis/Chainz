using Chainz.Tests.Shared;
using Xunit;

namespace Chainz.Tests.Unit
{
    public class After
    {
        [Fact]
        public void AddsChainLinkAfter()
        {
            var chain = new Chain<ChainParam>()
                .Use<FirstChainLink>()
                .Use<ThirdChainLink>()
                .After<FirstChainLink, SecondChainLink>()
                .Compile();
            Assert.IsType<FirstChainLink>(chain);
            Assert.IsType<SecondChainLink>(chain.Next);
            Assert.IsType<ThirdChainLink>(chain.Next.Next);
        }
    }
}