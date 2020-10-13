namespace Chainz
{
    /// <summary>
    ///     All chainlinks should implement this interface
    /// </summary>
    /// <typeparam name="TArg">
    ///     The type of the parameter to be passed along the chain of handlers
    /// </typeparam>
    public interface IChainLink<TArg>
    {
        IChainLink<TArg> Next { get; set; }
        void Handle(TArg args);
    }
}