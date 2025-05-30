/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using FluentAssertions;
using IdentityServer9.Models;
using IdentityServer9.Test;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.IntegrationTests.Common;
using Xunit;

namespace IdentityServer.IntegrationTests.Endpoints.Token;

public class TokenEndpointTests
{
    private const string Category = "Token endpoint";

    private string client_id = "client";
    private string client_secret = "secret";

    private string scope_name = "api";
    private string scope_secret = "api_secret";

    private IdentityServerPipeline _mockPipeline = new IdentityServerPipeline();

    public TokenEndpointTests()
    {
        _mockPipeline.Clients.Add(new Client
        {
            ClientId = client_id,
            ClientSecrets = new List<Secret> { new Secret(client_secret.Sha256()) },
            AllowedGrantTypes = { GrantType.ClientCredentials, GrantType.ResourceOwnerPassword },
            AllowedScopes = new List<string> { "api" },
        });


        _mockPipeline.Users.Add(new TestUser
        {
            SubjectId = "bob",
            Username = "bob",
            Password = "password",
            Claims = new Claim[]
            {
                new Claim("name", "Bob Loblaw"),
                new Claim("email", "bob@loblaw.com"),
                new Claim("role", "Attorney")
            }
        });

        _mockPipeline.IdentityScopes.AddRange(new IdentityResource[] {
            new IdentityResources.OpenId()
        });

        _mockPipeline.ApiResources.AddRange(new ApiResource[] {
            new ApiResource
            {
                Name = "api",
                ApiSecrets = new List<Secret> { new Secret(scope_secret.Sha256()) },
                Scopes = {scope_name}
            }
        });

        _mockPipeline.ApiScopes.AddRange(new[] {
            new ApiScope
            {
                Name = scope_name
            }
        });

        _mockPipeline.Initialize();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task client_credentials_request_with_funny_headers_should_not_hang()
    {
        var data = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", client_id },
            { "client_secret", client_secret },
            { "scope", scope_name },
        };
        var form = new FormUrlEncodedContent(data);
        _mockPipeline.BackChannelClient.DefaultRequestHeaders.Add("Referer", "http://127.0.0.1:33086/appservice/appservice?t=1564165664142?load");
        var response = await _mockPipeline.BackChannelClient.PostAsync(IdentityServerPipeline.TokenEndpoint, form);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.Content.ReadAsStringAsync();
        var result = JObject.Parse(json);
        result.ContainsKey("error").Should().BeFalse();
    }

    [Fact]
    [Trait("Category", Category)]
    public async Task resource_owner_request_with_funny_headers_should_not_hang()
    {
        var data = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", "bob" },
            { "password", "password" },
            { "client_id", client_id },
            { "client_secret", client_secret },
            { "scope", scope_name },
        };
        var form = new FormUrlEncodedContent(data);
        _mockPipeline.BackChannelClient.DefaultRequestHeaders.Add("Referer", "http://127.0.0.1:33086/appservice/appservice?t=1564165664142?load");
        var response = await _mockPipeline.BackChannelClient.PostAsync(IdentityServerPipeline.TokenEndpoint, form);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.Content.ReadAsStringAsync();
        var result = JObject.Parse(json);
        result.ContainsKey("error").Should().BeFalse();
    }
}
