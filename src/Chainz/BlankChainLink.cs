namespace Chainz
{
    public class BlankChainLink<TArg> : IChainLink<TArg>
    {
        public IChainLink<TArg> Next { get; set; }
        public void Handle(TArg args) { }
    }
}