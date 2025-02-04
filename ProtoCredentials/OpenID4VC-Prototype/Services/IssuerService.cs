﻿using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Utils;
using Serilog;

namespace OpenID4VC_Prototype.Services
{
    public class IssuerService
    {
        public VerifiableCredential IssuerCredential(DecentralizedIdentifier issuer, string holderDId)
        {
            Log.Information($"Issuing credential for holder DID: {holderDId}");

            if (!DIdUtils.IsValidDId(holderDId))
                throw new ArgumentException($"Invalid holder DID: {holderDId}");

            var credential = new VerifiableCredential
            {
                IssuerDId = issuer.DId,
                HolderDId = holderDId,
                CredentialType = "Diploma",
                Claims = new Dictionary<string, string>
                {
                    { "University", "PMF" },
                    { "Curriculum", "Mathematics" }
                }
            };

            credential.Signature = CryptoUtils.SignData(credential, issuer.PrivateKey);

            return credential;
        }
    }
}
