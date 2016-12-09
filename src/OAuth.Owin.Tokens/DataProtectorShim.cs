using System;

namespace OAuth.Owin.Tokens
{

    /// <summary>
    /// Converts a <see cref="Microsoft.AspNetCore.DataProtection.IDataProtector"/> to a <see cref="Microsoft.Owin.Security.DataProtection.IDataProtector"/>.
    /// </summary>
    internal sealed class DataProtectorShim : Microsoft.Owin.Security.DataProtection.IDataProtector
    {
        private readonly Microsoft.AspNetCore.DataProtection.IDataProtector _protector;

        public DataProtectorShim(Microsoft.AspNetCore.DataProtection.IDataProtector protector)
        {
            if (protector == null)
                throw new ArgumentNullException(nameof(protector));

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