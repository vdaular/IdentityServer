/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServerHost.Configuration;

public static class ClientsWeb
{
    static string[] allowedScopes = 
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        IdentityServerConstants.StandardScopes.Email,
        "resource1.scope1", 
        "resource2.scope1",
        "transaction"
    };
    
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
        {
            ///////////////////////////////////////////
            // JS OIDC Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "js_oidc",
                ClientName = "JavaScript OIDC Client",
                ClientUri = "http://IdentityServer9.io",
                
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                
                RedirectUris = 
                {
                    "https://localhost:44300/index.html",
                    "https://localhost:44300/callback.html",
                    "https://localhost:44300/silent.html",
                    "https://localhost:44300/popup.html"
                },

                PostLogoutRedirectUris = { "https://localhost:44300/index.html" },
                AllowedCorsOrigins = { "https://localhost:44300" },

                AllowedScopes = allowedScopes
            },
            
            ///////////////////////////////////////////
            // MVC Automatic Token Management Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "mvc.tokenmanagement",
                
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                AccessTokenLifetime = 75,

                RedirectUris = { "https://localhost:44301/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44301/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44301/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = allowedScopes
            },
            
            ///////////////////////////////////////////
            // MVC Code Flow Sample
            //////////////////////////////////////////
            new Client
            {
                ClientId = "mvc.code",
                ClientName = "MVC Code Flow",
                ClientUri = "http://IdentityServer9.io",

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                RequireConsent = true,
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44302/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44302/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44302/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = allowedScopes
            },
            
            ///////////////////////////////////////////
            // MVC Hybrid Flow Sample (Back Channel logout)
            //////////////////////////////////////////
            new Client
            {
                ClientId = "mvc.hybrid.backchannel",
                ClientName = "MVC Hybrid (with BackChannel logout)",
                ClientUri = "http://IdentityServer9.io",

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = false,

                RedirectUris = { "https://localhost:44303/signin-oidc" },
                BackChannelLogoutUri = "https://localhost:44303/logout",
                PostLogoutRedirectUris = { "https://localhost:44303/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = allowedScopes
            }
        };
    }
}
