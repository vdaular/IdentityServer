/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer9.Models;
using IdentityServer9.Stores;
using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace IdentityServer.UnitTests.Stores
{
    public class InMemoryResourcesStoreTests
    {
        [Fact]
        public void InMemoryResourcesStore_should_throw_if_contains_duplicate_names()
        {
            List<IdentityResource> identityResources = new List<IdentityResource>
            {
                new IdentityResource { Name = "A" },
                new IdentityResource { Name = "A" },
                new IdentityResource { Name = "C" }
            };

            List<ApiResource> apiResources = new List<ApiResource>
            {
                new ApiResource { Name = "B" },
                new ApiResource { Name = "B" },
                new ApiResource { Name = "C" }
            };

            List<ApiScope> scopes = new List<ApiScope>
            {
                new ApiScope { Name = "B" },
                new ApiScope { Name = "C" },
                new ApiScope { Name = "C" },
            };

            Action act = () => new InMemoryResourcesStore(identityResources, null, null);
            act.Should().Throw<ArgumentException>();

            act = () => new InMemoryResourcesStore(null, apiResources, null);
            act.Should().Throw<ArgumentException>();
            
            act = () => new InMemoryResourcesStore(null, null, scopes);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void InMemoryResourcesStore_should_not_throw_if_does_not_contains_duplicate_names()
        {
            List<IdentityResource> identityResources = new List<IdentityResource>
            {
                new IdentityResource { Name = "A" },
                new IdentityResource { Name = "B" },
                new IdentityResource { Name = "C" }
            };

            List<ApiResource> apiResources = new List<ApiResource>
            {
                new ApiResource { Name = "A" },
                new ApiResource { Name = "B" },
                new ApiResource { Name = "C" }
            };

            List<ApiScope> apiScopes = new List<ApiScope>
            {
                new ApiScope { Name = "A" },
                new ApiScope { Name = "B" },
                new ApiScope { Name = "C" },
            };
            
            new InMemoryResourcesStore(identityResources, null, null);
            new InMemoryResourcesStore(null, apiResources, null);
            new InMemoryResourcesStore(null, null, apiScopes);
        }
    }
}
