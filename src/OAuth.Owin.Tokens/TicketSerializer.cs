using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Serializer;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace OAuth.Owin.Tokens
{

    public class TicketSerializer : IDataSerializer<AuthenticationTicket>
    {
        #region non-Public Members

        private const string DefaultStringPlaceholder = "\0";

        private const int FormatVersion = 5;

        private static string ReadWithDefault(BinaryReader reader, string defaultValue)
        {
            var value = reader.ReadString();
            if (string.Equals(value, DefaultStringPlaceholder, StringComparison.Ordinal))
            {
                return defaultValue;
            }
            return value;
        }

        private static void WriteWithDefault(BinaryWriter writer, string value, string defaultValue)
        {
            if (string.Equals(value, defaultValue, StringComparison.Ordinal))
            {
                writer.Write(DefaultStringPlaceholder);
            }
            else
            {
                writer.Write(value);
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static void EnsureNotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }

        protected virtual void WriteIdentity(BinaryWriter writer, ClaimsIdentity identity)
        {
            EnsureNotNull(writer, nameof(writer));

            EnsureNotNull(identity, nameof(identity));

            var authenticationType = identity.AuthenticationType ?? string.Empty;

            writer.Write(authenticationType);

            WriteWithDefault(writer, identity.NameClaimType, ClaimsIdentity.DefaultNameClaimType);

            WriteWithDefault(writer, identity.RoleClaimType, ClaimsIdentity.DefaultRoleClaimType);

            // Write the number of claims contained in the identity.
            writer.Write(identity.Claims.Count());

            foreach (var claim in identity.Claims)
            {
                WriteClaim(writer, claim);
            }

            var bootstrap = identity.BootstrapContext as string;
            if (!string.IsNullOrEmpty(bootstrap))
            {
                writer.Write(true);
                writer.Write(bootstrap);
            }
            else
            {
                writer.Write(false);
            }

            if (identity.Actor != null)
            {
                writer.Write(true);
                WriteIdentity(writer, identity.Actor);
            }
            else
            {
                writer.Write(false);
            }
        }

        protected virtual void WriteClaim(BinaryWriter writer, Claim claim)
        {
            EnsureNotNull(writer, nameof(writer));

            EnsureNotNull(claim, nameof(claim));

            WriteWithDefault(writer, claim.Type, claim.Subject?.NameClaimType ?? ClaimsIdentity.DefaultNameClaimType);

            writer.Write(claim.Value);

            WriteWithDefault(writer, claim.ValueType, ClaimValueTypes.String);

            WriteWithDefault(writer, claim.Issuer, ClaimsIdentity.DefaultIssuer);

            WriteWithDefault(writer, claim.OriginalIssuer, claim.Issuer);

            // Write the number of properties contained in the claim.
            writer.Write(claim.Properties.Count);

            foreach (var property in claim.Properties)
            {
                writer.Write(property.Key ?? string.Empty);
                writer.Write(property.Value ?? string.Empty);
            }
        }

        protected virtual ClaimsIdentity ReadIdentity(BinaryReader reader)
        {
            EnsureNotNull(reader, nameof(reader));

            var authenticationType = reader.ReadString();

            var nameClaimType = ReadWithDefault(reader, ClaimsIdentity.DefaultNameClaimType);

            var roleClaimType = ReadWithDefault(reader, ClaimsIdentity.DefaultRoleClaimType);

            // Read the number of claims contained
            // in the serialized identity.
            var count = reader.ReadInt32();

            var identity = new ClaimsIdentity(authenticationType, nameClaimType, roleClaimType);

            for (int index = 0; index != count; ++index)
            {
                var claim = ReadClaim(reader, identity);

                identity.AddClaim(claim);
            }

            // Determine whether the identity
            // has a bootstrap context attached.
            if (reader.ReadBoolean())
            {
                identity.BootstrapContext = reader.ReadString();
            }

            // Determine whether the identity
            // has an actor identity attached.
            if (reader.ReadBoolean())
            {
                identity.Actor = ReadIdentity(reader);
            }

            return identity;
        }

        protected virtual Claim ReadClaim(BinaryReader reader, ClaimsIdentity identity)
        {
            EnsureNotNull(reader, nameof(reader));

            EnsureNotNull(identity, nameof(identity));

            var type = ReadWithDefault(reader, identity.NameClaimType);
            var value = reader.ReadString();
            var valueType = ReadWithDefault(reader, ClaimValueTypes.String);
            var issuer = ReadWithDefault(reader, ClaimsIdentity.DefaultIssuer);
            var originalIssuer = ReadWithDefault(reader, issuer);

            var claim = new Claim(type, value, valueType, issuer, originalIssuer, identity);

            // Read the number of properties stored in the claim.
            var count = reader.ReadInt32();

            for (var index = 0; index != count; ++index)
            {
                var key = reader.ReadString();
                var propertyValue = reader.ReadString();

                claim.Properties.Add(key, propertyValue);
            }

            return claim;
        }

        #endregion

        #region Public Members

        public static TicketSerializer Default { get; } = new TicketSerializer();

        public virtual byte[] Serialize(AuthenticationTicket ticket)
        {
            using (var memory = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memory))
                {
                    Write(writer, ticket);
                }
                return memory.ToArray();
            }
        }

        public virtual AuthenticationTicket Deserialize(byte[] data)
        {
            using (var memory = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(memory))
                {
                    return Read(reader);
                }
            }
        }

        public virtual void Write(BinaryWriter writer, AuthenticationTicket ticket)
        {
            EnsureNotNull(writer, nameof(writer));

            EnsureNotNull(ticket, nameof(ticket));

            writer.Write(FormatVersion);

            writer.Write(ticket.Identity.AuthenticationType);

            var identity = ticket.Identity;
            if (identity == null)
                throw new ArgumentNullException(nameof(ticket.Identity));

            // Only 1 identity possible.
            writer.Write(1);

            WriteIdentity(writer, identity);

            PropertiesSerializer.Write(writer, ticket.Properties);
        }

        public virtual AuthenticationTicket Read(BinaryReader reader)
        {
            EnsureNotNull(reader, nameof(reader));

            if (reader.ReadInt32() != FormatVersion)
            {
                return null;
            }

            var scheme = reader.ReadString();

            // Read the number of identities stored
            // in the serialized payload.
            var count = reader.ReadInt32();
            if (count < 0)
            {
                return null;
            }

            ClaimsIdentity identity = ReadIdentity(reader);

            var properties = PropertiesSerializer.Read(reader);

            return new AuthenticationTicket(identity, properties);
        }

        #endregion
    }

}