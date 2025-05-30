/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer9.EntityFramework.DbContexts;
using IdentityServer9.EntityFramework.Interfaces;
using IdentityServer9.EntityFramework.Mappers;
using IdentityServer9.EntityFramework.Options;
using IdentityServer9.EntityFramework.Services;
using IdentityServer9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IdentityServer9.EntityFramework.IntegrationTests.Services;

public class CorsPolicyServiceTests : IntegrationTest<CorsPolicyServiceTests, ConfigurationDbContext, ConfigurationStoreOptions>
{
    public CorsPolicyServiceTests(DatabaseProviderFixture<ConfigurationDbContext> fixture) : base(fixture)
    {
        foreach (var provider in TestDatabaseProviders)
        {
            using (var context = new ConfigurationDbContext((DbContextOptions<ConfigurationDbContext>) provider, StoreOptions))
            {
                context.Database.EnsureCreated();
            }
        }
    }

    [Theory, MemberData(nameof(TestDatabaseProviders))]
    public async Task IsOriginAllowedAsync_WhenOriginIsAllowed_ExpectTrue(DbContextOptions<ConfigurationDbContext> options)
    {
        const string testCorsOrigin = "https://IdentityServer9.io/";

        using (var context = new ConfigurationDbContext(options, StoreOptions))
        {
            context.Clients.Add(new Client
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = Guid.NewGuid().ToString(),
                AllowedCorsOrigins = new List<string> { "https://www.IdentityServer9.com" }
            }.ToEntity());
            context.Clients.Add(new Client
            {
                ClientId = "2",
                ClientName = "2",
                AllowedCorsOrigins = new List<string> { "https://www.IdentityServer9.com", testCorsOrigin }
            }.ToEntity());
            context.SaveChanges();
        }

        bool result;
        using (var context = new ConfigurationDbContext(options, StoreOptions))
        {
            var ctx = new DefaultHttpContext();
            var svcs = new ServiceCollection();
            svcs.AddSingleton<IConfigurationDbContext>(context);
            ctx.RequestServices = svcs.BuildServiceProvider();
            var ctxAccessor = new HttpContextAccessor();
            ctxAccessor.HttpContext = ctx;

            var service = new CorsPolicyService(ctxAccessor, FakeLogger<CorsPolicyService>.Create());
            result = await service.IsOriginAllowedAsync(testCorsOrigin);
        }

        Assert.True(result);
    }

    [Theory, MemberData(nameof(TestDatabaseProviders))]
    public async Task IsOriginAllowedAsync_WhenOriginIsNotAllowed_ExpectFalse(DbContextOptions<ConfigurationDbContext> options)
    {
        using (var context = new ConfigurationDbContext(options, StoreOptions))
        {
            context.Clients.Add(new Client
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = Guid.NewGuid().ToString(),
                AllowedCorsOrigins = new List<string> { "https://www.IdentityServer9.com" }
            }.ToEntity());
            context.SaveChanges();
        }

        bool result;
        using (var context = new ConfigurationDbContext(options, StoreOptions))
        {
            var ctx = new DefaultHttpContext();
            var svcs = new ServiceCollection();
            svcs.AddSingleton<IConfigurationDbContext>(context);
            ctx.RequestServices = svcs.BuildServiceProvider();
            var ctxAccessor = new HttpContextAccessor();
            ctxAccessor.HttpContext = ctx;

            var service = new CorsPolicyService(ctxAccessor, FakeLogger<CorsPolicyService>.Create());
            result = await service.IsOriginAllowedAsync("InvalidOrigin");
        }

        Assert.False(result);
    }
}
