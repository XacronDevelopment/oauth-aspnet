using Microsoft.Framework.Internal;

namespace OAuth.Owin.Tokens
{

    /// <summary>
    /// Converts a <see cref="Microsoft.AspNet.DataProtection.IDataProtector"/> to a <see cref="Microsoft.Owin.Security.DataProtection.IDataProtector"/>.
    /// </summary>
    internal sealed class DataProtectorShim : Microsoft.Owin.Security.DataProtection.IDataProtector
    {
        private readonly Microsoft.AspNet.DataProtection.IDataProtector _protector;

        public DataProtectorShim([NotNull] Microsoft.AspNet.DataProtection.IDataProtector protector)
        {
            _protector = protector;
        }

        public byte[] Protect(byte[] userData)
        {
            return _protector.Protect(userData);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return _protector.Unprotect(protectedData);
        }
    }

}