/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

global using IdentityModel;
global using IdentityServer9;
global using IdentityServer9.Configuration;
global using IdentityServer9.Events;
global using IdentityServer9.Extensions;
global using IdentityServer9.Models;
global using IdentityServer9.Services;
global using IdentityServer9.Stores;
global using IdentityServer9.Test;
global using IdentityServer9.Validation;
global using IdentityServerHost.Configuration;
global using IdentityServerHost.Data;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Newtonsoft.Json;
global using Serilog;
global using Serilog.Events;
global using Serilog.Sinks.SystemConsole.Themes;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Security.Claims;
global using System.Text;
global using static IdentityServer9.IdentityServerConstants;
global using ILogger = Microsoft.Extensions.Logging.ILogger;
global using ClaimValueTypes = System.Security.Claims.ClaimValueTypes;
