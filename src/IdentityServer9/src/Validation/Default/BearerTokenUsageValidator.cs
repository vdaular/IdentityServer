/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer9.Validation;

/// <summary>
/// Validates a request that uses a bearer token for authentication
/// </summary>
internal class BearerTokenUsageValidator
{
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BearerTokenUsageValidator"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public BearerTokenUsageValidator(ILogger<BearerTokenUsageValidator> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Validates the request.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public async Task<BearerTokenUsageValidationResult> ValidateAsync(HttpContext context)
    {
        var result = ValidateAuthorizationHeader(context);
        if (result.TokenFound)
        {
            _logger.LogDebug("Bearer token found in header");
            return result;
        }

        if (context.Request.HasApplicationFormContentType())
        {
            result = await ValidatePostBodyAsync(context);
            if (result.TokenFound)
            {
                _logger.LogDebug("Bearer token found in body");
                return result;
            }
        }

        _logger.LogDebug("Bearer token not found");
        return new BearerTokenUsageValidationResult();
    }

    /// <summary>
    /// Validates the authorization header.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public BearerTokenUsageValidationResult ValidateAuthorizationHeader(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader.IsPresent())
        {
            var header = authorizationHeader.Trim();
            if (header.StartsWith(OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer))
            {
                var value = header.Substring(OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer.Length).Trim();
                if (value.IsPresent())
                {
                    return new BearerTokenUsageValidationResult
                    {
                        TokenFound = true,
                        Token = value,
                        UsageType = BearerTokenUsageType.AuthorizationHeader
                    };
                }
            }
            else
            {
                _logger.LogTrace("Unexpected header format: {header}", Ioc.Sanitizer.Log.Sanitize(header));
            }
        }

        return new BearerTokenUsageValidationResult();
    }

    /// <summary>
    /// Validates the post body.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public async Task<BearerTokenUsageValidationResult> ValidatePostBodyAsync(HttpContext context)
    {
        var token = (await context.Request.ReadFormAsync())["access_token"].FirstOrDefault();
        if (token.IsPresent())
        {
            return new BearerTokenUsageValidationResult
            {
                TokenFound = true,
                Token = token,
                UsageType = BearerTokenUsageType.PostBody
            };
        }

        return new BearerTokenUsageValidationResult();
    }
}
