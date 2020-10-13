using Chainz.Tests.Shared;
using Xunit;

namespace Chainz.Tests.Unit
{
    public class Before
    {
        [Fact]
        public void AddsChainLinkBefore()
        {
            var chain = new Chain<ChainParam>()
                .Use<SecondChainLink>()
                .Use<ThirdChainLink>()
                .Before<SecondChainLink, FirstChainLink>()
                .Compile();
            Assert.IsType<FirstChainLink>(chain);
            Assert.IsType<SecondChainLink>(chain.Next);
            Assert.IsType<ThirdChainLink>(chain.Next.Next);
        }
    }
}