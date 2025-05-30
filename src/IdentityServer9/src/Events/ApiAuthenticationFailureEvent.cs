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
/// Event for failed API authentication
/// </summary>
/// <seealso cref="IdentityServer9.Events.Event" />
public class ApiAuthenticationFailureEvent : Event
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiAuthenticationFailureEvent"/> class.
    /// </summary>
    /// <param name="apiName">Name of the API.</param>
    /// <param name="message">The message.</param>
    public ApiAuthenticationFailureEvent(string apiName, string message)
        : base(EventCategories.Authentication, 
              "API Authentication Failure",
              EventTypes.Failure, 
              EventIds.ApiAuthenticationFailure, 
              message)
    {
        ApiName = apiName;
    }

    /// <summary>
    /// Gets or sets the name of the API.
    /// </summary>
    /// <value>
    /// The name of the API.
    /// </value>
    public string ApiName { get; set; }
}
