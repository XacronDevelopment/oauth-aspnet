using Microsoft.AspNetCore.DataProtection;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.IO;

namespace OAuth.Owin.Tokens
{

    public class TicketDataFormat : SecureDataFormat<AuthenticationTicket>
    {
        public TicketDataFormat(Microsoft.Owin.Security.DataProtection.IDataProtector protector = null) : base(
                                                                                                                  new TicketSerializer(), 
                                                                                                                  protector ?? new DataProtectorShim((DataProtectionProvider.Create(new DirectoryInfo(Environment.GetEnvironmentVariable("Temp"))).CreateProtector("OAuth.AspNet.AuthServer", "Access_Token", "v1"))), 
                                                                                                                  TextEncodings.Base64Url
                                                                                                              )
        {

        }
    }

}