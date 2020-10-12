namespace Chainz
{
    public interface IChainLink<TArg>
    {
        IChainLink<TArg> Next { get; set; }
        void Handle(TArg args);
    }
}