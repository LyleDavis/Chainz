using System;

namespace Chainz.Utils
{
    public static class Reflector
    {
        public static TType Instantiate<TType>(Type klass) where TType : class
        {
            return (TType) Activator.CreateInstance(klass);
        }
    }
}