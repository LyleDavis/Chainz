using Chainz.Tests.Shared;
using Xunit;

namespace Chainz.Tests.Unit
{
    public class Use
    {
        [Fact]
        public void AppendsChainLink()
        {
            var chain = new Chain<ChainParam>()
                .Use<FirstChainLink>()
                .Use<SecondChainLink>()
                .Compile();
            Assert.IsType<FirstChainLink>(chain);
            Assert.IsType<SecondChainLink>(chain.Next);
        }
    }
}