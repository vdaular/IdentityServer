/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace Microsoft.AspNetCore.Http;

/// <summary>
/// Extension methods for signin/out using the IdentityServer authentication scheme.
/// </summary>
public static class AuthenticationManagerExtensions
{
    /// <summary>
    /// Signs the user in.
    /// </summary>
    /// <param name="context">The manager.</param>
    /// <param name="user">The IdentityServer user.</param>
    /// <returns></returns>
    public static async Task SignInAsync(this HttpContext context, IdentityServerUser user)
    {
        await context.SignInAsync(await context.GetCookieAuthenticationSchemeAsync(), user.CreatePrincipal());
    }

    /// <summary>
    /// Signs the user in.
    /// </summary>
    /// <param name="context">The manager.</param>
    /// <param name="user">The IdentityServer user.</param>
    /// <param name="properties">The authentication properties.</param>
    /// <returns></returns>
    public static async Task SignInAsync(this HttpContext context, IdentityServerUser user, AuthenticationProperties properties)
    {
        await context.SignInAsync(await context.GetCookieAuthenticationSchemeAsync(), user.CreatePrincipal(), properties);
    }

    internal static ISystemClock GetClock(this HttpContext context)
    {
        return context.RequestServices.GetRequiredService<ISystemClock>();
    }

    internal static async Task<string> GetCookieAuthenticationSchemeAsync(this HttpContext context)
    {
        var options = context.RequestServices.GetRequiredService<IdentityServerOptions>();
        if (options.Authentication.CookieAuthenticationScheme != null)
        {
            return options.Authentication.CookieAuthenticationScheme;
        }

        var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
        var scheme = await schemes.GetDefaultAuthenticateSchemeAsync();
        if (scheme == null)
        {
            throw new InvalidOperationException("No DefaultAuthenticateScheme found or no CookieAuthenticationScheme configured on IdentityServerOptions.");
        }

        return scheme.Name;
    }
}
