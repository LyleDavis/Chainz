using System.Collections.Generic;
using Chainz.Tests.Shared;
using Xunit;

namespace Chainz.Tests
{
    public class ChainTests
    {
        [Fact]
        public void Chain_RunsThroughTheChain()
        {
            var chain = new Chain<ChainParam>()
                .Use<SecondChainLink>()
                .After<SecondChainLink, FourthChainLink>()
                .Prepend<FirstChainLink>()
                .Before<FourthChainLink, ThirdChainLink>()
                .Use<DoesNotContinueChainLink>()
                .Replace<DoesNotContinueChainLink, FifthChainLink>()
                .Use<DoesNotContinueChainLink>()
                .Use<SixthChainLink>()
                .Compile();
            var p = new ChainParam();
            Assert.Equal(p.Values, new List<int>());
            chain.Handle(p);
            Assert.Equal(p.Values, new List<int> {1, 2, 3, 4, 5});
        }

        [Fact]
        public void Chain_WhenLastLinkCallsHandle_DoesNotThrow()
        {
            var chain = new Chain<ChainParam>()
                .Use<FirstChainLink>()
                .Use<SecondChainLink>()
                .Compile();
            var p = new ChainParam();
            chain.Handle(p);
        }
    }
}