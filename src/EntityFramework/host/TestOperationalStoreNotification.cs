/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer9.EntityFramework;
using IdentityServer9.EntityFramework.Entities;
using PersistedGrant = IdentityServer9.EntityFramework.Entities.PersistedGrant;

namespace IdentityServerHost
{
    public class TestOperationalStoreNotification : IOperationalStoreNotification
    {
        public TestOperationalStoreNotification()
        {
            Console.WriteLine("ctor");
        }

        public Task PersistedGrantsRemovedAsync(IEnumerable<PersistedGrant> persistedGrants)
        {
            foreach (var grant in persistedGrants)
            {
                Console.WriteLine("cleaned: " + grant.Type);
            }
            return Task.CompletedTask;
        }

        public Task DeviceCodesRemovedAsync(IEnumerable<DeviceFlowCodes> deviceCodes)
        {
            foreach (var deviceCode in deviceCodes) 
            {
                Console.WriteLine("cleaned device code");
            }
            return Task.CompletedTask;
        }
    }
}
