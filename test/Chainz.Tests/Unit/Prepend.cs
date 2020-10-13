using Chainz.Tests.Shared;
using Xunit;

namespace Chainz.Tests.Unit
{
    public class Prepend
    {
        [Fact]
        public void PrependsChainLink()
        {
            var chain = new Chain<ChainParam>()
                .Use<SecondChainLink>()
                .Use<ThirdChainLink>()
                .Prepend<FirstChainLink>()
                .Compile();
            Assert.IsType<FirstChainLink>(chain);
            Assert.IsType<SecondChainLink>(chain.Next);
            Assert.IsType<ThirdChainLink>(chain.Next.Next);
        }
    }
}