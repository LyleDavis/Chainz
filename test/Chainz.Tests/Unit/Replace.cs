using Chainz.Tests.Shared;
using Xunit;

namespace Chainz.Tests.Unit
{
    public class Replace
    {
        [Fact]
        public void ReplacesChainLinkWithOtherChainLink()
        {
            var chain = new Chain<ChainParam>()
                .Use<FirstChainLink>()
                .Use<ThirdChainLink>()
                .Replace<ThirdChainLink, SecondChainLink>()
                .Compile();
            Assert.IsType<FirstChainLink>(chain);
            Assert.IsType<SecondChainLink>(chain.Next);
            // checking it hasn't be shunted off another level
            Assert.IsNotType<ThirdChainLink>(chain.Next.Next);
        }
    }
}