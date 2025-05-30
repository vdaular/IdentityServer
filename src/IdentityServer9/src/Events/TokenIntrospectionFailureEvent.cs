/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer9.Events;

/// <summary>
/// Event for failed token introspection
/// </summary>
/// <seealso cref="IdentityServer9.Events.Event" />
public class TokenIntrospectionFailureEvent : Event
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TokenIntrospectionSuccessEvent" /> class.
    /// </summary>
    /// <param name="apiName">Name of the API.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="token">The token.</param>
    /// <param name="apiScopes">The API scopes.</param>
    /// <param name="tokenScopes">The token scopes.</param>
    public TokenIntrospectionFailureEvent(string apiName, string errorMessage, string token = null, IEnumerable<string> apiScopes = null, IEnumerable<string> tokenScopes = null)
        : base(EventCategories.Token,
              "Token Introspection Failure",
              EventTypes.Failure,
              EventIds.TokenIntrospectionFailure,
              errorMessage)
    {
        ApiName = apiName;

        if (token.IsPresent())
        {
            Token = Obfuscate(token);
        }

        if (apiScopes != null)
        {
            ApiScopes = apiScopes;
        }

        if (tokenScopes != null)
        {
            TokenScopes = tokenScopes;
        }
    }

    /// <summary>
    /// Gets or sets the name of the API.
    /// </summary>
    /// <value>
    /// The name of the API.
    /// </value>
    public string ApiName { get; set; }

    /// <summary>
    /// Gets or sets the token.
    /// </summary>
    /// <value>
    /// The token.
    /// </value>
    public string Token { get; set; }

    /// <summary>
    /// Gets or sets the API scopes.
    /// </summary>
    /// <value>
    /// The API scopes.
    /// </value>
    public IEnumerable<string> ApiScopes { get; set; }

    /// <summary>
    /// Gets or sets the token scopes.
    /// </summary>
    /// <value>
    /// The token scopes.
    /// </value>
    public IEnumerable<string> TokenScopes { get; set; }
}
