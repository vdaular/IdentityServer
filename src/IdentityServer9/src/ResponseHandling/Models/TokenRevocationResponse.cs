/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer9.ResponseHandling;

/// <summary>
/// Models a token revocation response
/// </summary>
public class TokenRevocationResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the token revocation was successful.
    /// </summary>
    /// <value>
    ///   <c>true</c> if success; otherwise, <c>false</c>.
    /// </value>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the type of the token that was revoked.
    /// </summary>
    /// <value>
    /// The type of the token.
    /// </value>
    public string TokenType { get; set; }

    /// <summary>
    /// Gets or sets an error (if present).
    /// </summary>
    /// <value>
    /// The error.
    /// </value>
    public string Error { get; set; }
}
