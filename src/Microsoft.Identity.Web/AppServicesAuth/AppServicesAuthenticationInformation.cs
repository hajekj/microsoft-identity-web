﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;

namespace Microsoft.Identity.Web
{
    /// <summary>
    /// Information about the App Services configuration on the host.
    /// </summary>
    public static class AppServicesAuthenticationInformation
    {
        // Environment variables.
        private const string AppServicesAuthEnabledEnvironmentVariable = "WEBSITE_AUTH_ENABLED";            // True
        private const string AppServicesAuthOpenIdIssuerEnvironmentVariable = "WEBSITE_AUTH_OPENID_ISSUER"; // for instance https://sts.windows.net/<tenantId>/
        private const string AppServicesAuthClientIdEnvironmentVariable = "WEBSITE_AUTH_CLIENT_ID";         // A GUID
        private const string AppServicesAuthClientSecretEnvironmentVariable = "WEBSITE_AUTH_CLIENT_SECRET"; // A string
        private const string AppServicesAuthLogoutPathEnvironmentVariable = "WEBSITE_AUTH_LOGOUT_PATH";    // /.auth/logout
        private const string AppServicesAuthIdentityProviderEnvironmentVariable = "WEBSITE_AUTH_DEFAULT_PROVIDER"; // AzureActiveDirectory
        private const string AppServicesAuthAzureActiveDirectory = "AzureActiveDirectory";

        // Artificially added by Microsoft.Identity.Web to help debugging App Services. See the Debug controller of the test app
        private const string AppServicesAuthDebugHeadersEnvironmentVariable = "APP_SERVICES_AUTH_LOCAL_DEBUG";

        /// <summary>
        /// Is App Services authentication enabled?.
        /// </summary>
        public static bool IsAppServicesAadAuthenticationEnabled
        {
            get
            {
                return (Environment.GetEnvironmentVariable(AppServicesAuthEnabledEnvironmentVariable) == Constants.True)
                    && Environment.GetEnvironmentVariable(AppServicesAuthIdentityProviderEnvironmentVariable) == AppServicesAuthAzureActiveDirectory;
            }
        }

        /// <summary>
        /// Logout URL for App Services Auth web sites.
        /// </summary>
        public static string? LogoutUrl
        {
            get
            {
                return Environment.GetEnvironmentVariable(AppServicesAuthLogoutPathEnvironmentVariable);
            }
        }

        /// <summary>
        /// ClientID of the App Services Auth web site.
        /// </summary>
        internal static string? ClientId
        {
            get
            {
                return Environment.GetEnvironmentVariable(AppServicesAuthClientIdEnvironmentVariable);
            }
        }

        /// <summary>
        /// Client secret of the App Services Auth web site.
        /// </summary>
        internal static string? ClientSecret
        {
            get
            {
                return Environment.GetEnvironmentVariable(AppServicesAuthClientSecretEnvironmentVariable);
            }
        }

        /// <summary>
        /// Issuer of the App Services Auth web site.
        /// </summary>
        internal static string? Issuer
        {
            get
            {
                return Environment.GetEnvironmentVariable(AppServicesAuthOpenIdIssuerEnvironmentVariable);
            }
        }

#if DEBUG
        /// <summary>
        /// Get headers from environment to help debugging App Services authentication.
        /// </summary>
        internal static string? SimulateGetttingHeaderFromDebugEnvironmentVariable(string header)
        {
            string? headerPlusValue = Environment.GetEnvironmentVariable(AppServicesAuthDebugHeadersEnvironmentVariable)
                ?.Split(';')
                ?.FirstOrDefault(h => h.StartsWith(header));
            return headerPlusValue?.Substring(header.Length + 1);
        }
#endif
    }
}
