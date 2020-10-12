using System;

namespace Chainz.Utils
{
    internal static class Reflector
    {
        internal static TType Instantiate<TType>(Type klass) where TType : class
        {
            return (TType) Activator.CreateInstance(klass);
        }
    }
}