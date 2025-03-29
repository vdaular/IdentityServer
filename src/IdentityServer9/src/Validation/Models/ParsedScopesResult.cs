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
/// Represents the result of scope parsing.
/// </summary>
public class ParsedScopesResult
{
    /// <summary>
    /// The valid parsed scopes.
    /// </summary>
    public ICollection<ParsedScopeValue> ParsedScopes { get; set; } = new HashSet<ParsedScopeValue>();

    /// <summary>
    /// The errors encountered while parsing.
    /// </summary>
    public ICollection<ParsedScopeValidationError> Errors { get; set; } = new HashSet<ParsedScopeValidationError>();

    /// <summary>
    /// Indicates if the result of parsing the scopes was successful.
    /// </summary>
    public bool Succeeded => Errors == null || !Errors.Any();
}
