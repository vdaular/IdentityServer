/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using Microsoft.AspNetCore.DataProtection;

namespace IdentityServer9.Stores;

/// <summary>
/// IMessageStore implementation that uses data protection to protect message.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class ProtectedDataMessageStore<TModel> : IMessageStore<TModel>
{
    private const string Purpose = "IdentityServer9.Stores.ProtectedDataMessageStore";

    /// <summary>
    /// The data protector.
    /// </summary>
    protected readonly IDataProtector Protector;

    /// <summary>
    /// The logger.
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="logger"></param>
    public ProtectedDataMessageStore(IDataProtectionProvider provider, ILogger<ProtectedDataMessageStore<TModel>> logger)
    {
        Protector = provider.CreateProtector(Purpose);
        Logger = logger;
    }

    /// <inheritdoc />
    public virtual Task<Message<TModel>> ReadAsync(string value)
    {
        Message<TModel> result = null;

        if (!String.IsNullOrWhiteSpace(value))
        {
            try
            {
                var bytes = Base64Url.Decode(value);
                bytes = Protector.Unprotect(bytes);
                var json = Encoding.UTF8.GetString(bytes);
                result = ObjectSerializer.FromString<Message<TModel>>(json);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "Exception reading protected message");
            }
        }

        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public virtual Task<string> WriteAsync(Message<TModel> message)
    {
        string value = null;

        try
        {
            var json = ObjectSerializer.ToString(message);
            var bytes = Encoding.UTF8.GetBytes(json);
            bytes = Protector.Protect(bytes);
            value = Base64Url.Encode(bytes);
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Exception writing protected message");
        }

        return Task.FromResult(value);
    }
}
