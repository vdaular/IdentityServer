/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer9.EntityFramework.Mappers;
using IdentityServer9.Models;
using Xunit;

namespace IdentityServer9.EntityFramework.UnitTests.Mappers;

public class IdentityResourcesMappersTests
{
    [Fact]
    public void IdentityResourceAutomapperConfigurationIsValid()
    {
        IdentityResourceMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void CanMapIdentityResources()
    {
        var model = new IdentityResource();
        var mappedEntity = model.ToEntity();
        var mappedModel = mappedEntity.ToModel();

        Assert.NotNull(mappedModel);
        Assert.NotNull(mappedEntity);
    }
}
