/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer9.Extensions;
using Xunit;

namespace IdentityServer.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        private void CheckOrigin(string inputUrl, string expectedOrigin)
        {
            var actualOrigin = inputUrl.GetOrigin();
            Assert.Equal(expectedOrigin, actualOrigin);
        }

        [Fact]
        public void TestGetOrigin()
        {
            CheckOrigin("http://idsvr.com", "http://idsvr.com");
            CheckOrigin("http://idsvr.com/", "http://idsvr.com");
            CheckOrigin("http://idsvr.com/test", "http://idsvr.com");
            CheckOrigin("http://idsvr.com/test/resource", "http://idsvr.com");
            CheckOrigin("http://idsvr.com:8080", "http://idsvr.com:8080");
            CheckOrigin("http://idsvr.com:8080/", "http://idsvr.com:8080");
            CheckOrigin("http://idsvr.com:8080/test", "http://idsvr.com:8080");
            CheckOrigin("http://idsvr.com:8080/test/resource", "http://idsvr.com:8080");
            CheckOrigin("http://127.0.0.1", "http://127.0.0.1");
            CheckOrigin("http://127.0.0.1/", "http://127.0.0.1");
            CheckOrigin("http://127.0.0.1/test", "http://127.0.0.1");
            CheckOrigin("http://127.0.0.1/test/resource", "http://127.0.0.1");
            CheckOrigin("http://127.0.0.1:8080", "http://127.0.0.1:8080");
            CheckOrigin("http://127.0.0.1:8080/", "http://127.0.0.1:8080");
            CheckOrigin("http://127.0.0.1:8080/test", "http://127.0.0.1:8080");
            CheckOrigin("http://127.0.0.1:8080/test/resource", "http://127.0.0.1:8080");
            CheckOrigin("http://localhost", "http://localhost");
            CheckOrigin("http://localhost/", "http://localhost");
            CheckOrigin("http://localhost/test", "http://localhost");
            CheckOrigin("http://localhost/test/resource", "http://localhost");
            CheckOrigin("http://localhost:8080", "http://localhost:8080");
            CheckOrigin("http://localhost:8080/", "http://localhost:8080");
            CheckOrigin("http://localhost:8080/test", "http://localhost:8080");
            CheckOrigin("http://localhost:8080/test/resource", "http://localhost:8080");
            CheckOrigin("https://idsvr.com", "https://idsvr.com");
            CheckOrigin("https://idsvr.com/", "https://idsvr.com");
            CheckOrigin("https://idsvr.com/test", "https://idsvr.com");
            CheckOrigin("https://idsvr.com/test/resource", "https://idsvr.com");
            CheckOrigin("https://idsvr.com:8080", "https://idsvr.com:8080");
            CheckOrigin("https://idsvr.com:8080/", "https://idsvr.com:8080");
            CheckOrigin("https://idsvr.com:8080/test", "https://idsvr.com:8080");
            CheckOrigin("https://idsvr.com:8080/test/resource", "https://idsvr.com:8080");
            CheckOrigin("https://127.0.0.1", "https://127.0.0.1");
            CheckOrigin("https://127.0.0.1/", "https://127.0.0.1");
            CheckOrigin("https://127.0.0.1/test", "https://127.0.0.1");
            CheckOrigin("https://127.0.0.1/test/resource", "https://127.0.0.1");
            CheckOrigin("https://127.0.0.1:8080", "https://127.0.0.1:8080");
            CheckOrigin("https://127.0.0.1:8080/", "https://127.0.0.1:8080");
            CheckOrigin("https://127.0.0.1:8080/test", "https://127.0.0.1:8080");
            CheckOrigin("https://127.0.0.1:8080/test/resource", "https://127.0.0.1:8080");
            CheckOrigin("https://localhost", "https://localhost");
            CheckOrigin("https://localhost/", "https://localhost");
            CheckOrigin("https://localhost/test", "https://localhost");
            CheckOrigin("https://localhost/test/resource", "https://localhost");
            CheckOrigin("https://localhost:8080", "https://localhost:8080");
            CheckOrigin("https://localhost:8080/", "https://localhost:8080");
            CheckOrigin("https://localhost:8080/test", "https://localhost:8080");
            CheckOrigin("https://localhost:8080/test/resource", "https://localhost:8080");
            CheckOrigin("test://idsvr.com", null);
            CheckOrigin("test://idsvr.com/", null);
            CheckOrigin("test://idsvr.com/test", null);
            CheckOrigin("test://idsvr.com/test/resource", null);
            CheckOrigin("test://idsvr.com:8080", null);
            CheckOrigin("test://idsvr.com:8080/", null);
            CheckOrigin("test://idsvr.com:8080/test", null);
            CheckOrigin("test://idsvr.com:8080/test/resource", null);
            CheckOrigin("test://127.0.0.1", null);
            CheckOrigin("test://127.0.0.1/", null);
            CheckOrigin("test://127.0.0.1/test", null);
            CheckOrigin("test://127.0.0.1/test/resource", null);
            CheckOrigin("test://127.0.0.1:8080", null);
            CheckOrigin("test://127.0.0.1:8080/", null);
            CheckOrigin("test://127.0.0.1:8080/test", null);
            CheckOrigin("test://127.0.0.1:8080/test/resource", null);
            CheckOrigin("test://localhost", null);
            CheckOrigin("test://localhost/", null);
            CheckOrigin("test://localhost/test", null);
            CheckOrigin("test://localhost/test/resource", null);
            CheckOrigin("test://localhost:8080", null);
            CheckOrigin("test://localhost:8080/", null);
            CheckOrigin("test://localhost:8080/test", null);
            CheckOrigin("test://localhost:8080/test/resource", null);
        }
    }
}
