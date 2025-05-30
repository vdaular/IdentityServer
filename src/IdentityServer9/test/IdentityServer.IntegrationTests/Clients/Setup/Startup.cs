/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer9.Configuration;
using IdentityServer9.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.IntegrationTests.Clients.Setup;

public class Startup
{
    static public ICustomTokenRequestValidator CustomTokenRequestValidator { get; set; } 

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication();

        var builder = services.AddIdentityServer(options =>
        {
            options.IssuerUri = "https://idsvr8";

            options.Events = new EventsOptions
            {
                RaiseErrorEvents = true,
                RaiseFailureEvents = true,
                RaiseInformationEvents = true,
                RaiseSuccessEvents = true
            };
        });

        builder.AddInMemoryClients(Clients.Get());
        builder.AddInMemoryIdentityResources(Scopes.GetIdentityScopes());
        builder.AddInMemoryApiResources(Scopes.GetApiResources());
        builder.AddInMemoryApiScopes(Scopes.GetApiScopes());
        builder.AddTestUsers(Users.Get());

        builder.AddDeveloperSigningCredential(persistKey: false);

        builder.AddExtensionGrantValidator<ExtensionGrantValidator>();
        builder.AddExtensionGrantValidator<ExtensionGrantValidator2>();
        builder.AddExtensionGrantValidator<NoSubjectExtensionGrantValidator>();
        builder.AddExtensionGrantValidator<DynamicParameterExtensionGrantValidator>();

        builder.AddProfileService<CustomProfileService>();

        builder.AddJwtBearerClientAuthentication();
        builder.AddSecretValidator<ConfirmationSecretValidator>();

        // add a custom token request validator if set
        if (CustomTokenRequestValidator != null)
        {
            builder.Services.AddTransient(r => CustomTokenRequestValidator);
        }
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseIdentityServer();
    }
}
