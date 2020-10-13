namespace Chainz
{
    /// <summary>
    ///     A blank chainlink used to finish any chain of handlers.
    ///     This ensures that the last handler in a chain can be
    ///     reusable by allowing it to call `Next.Handle(args)` safely.
    /// </summary>
    internal class BlankChainLink<TArg> : IChainLink<TArg>
    {
        public IChainLink<TArg> Next { get; set; }

        public void Handle(TArg args)
        {
        }
    }
}