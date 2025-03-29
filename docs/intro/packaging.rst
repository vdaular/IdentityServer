Packaging and Builds
====================

IdentityServer consists of a number of nuget packages.

IdentityServer9 main repo
^^^^^^^^^^^^^^^
`github <https://github.com/alexhiggins732/IdentityServer9>`_

Contains the core IdentityServer object model, services and middleware as well as the EntityFramework and ASP.NET Identity integration.

nugets:

* `HigginsSoft.IdentityServer9 <https://www.nuget.org/packages/HigginsSoft.IdentityServer9/>`_
* `HigginsSoft.IdentityServer9.EntityFramework <https://www.nuget.org/packages/HigginsSoft.IdentityServer9.EntityFramework>`_
* `HigginsSoft.IdentityServer9.AspNetIdentity <https://www.nuget.org/packages/HigginsSoft.IdentityServer9.AspNetIdentity>`_

Quickstart UI
^^^^^^^^^^^^^
`github <https://github.com/alexhiggins732/IdentityServer9.Quickstart.UI>`_

Contains a simple starter UI including login, logout and consent pages.

Access token validation handler
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
`nuget <https://www.nuget.org/packages/HigginsSoft.IdentityServer9.AccessTokenValidation>`_ | `github <https://github.com/alexhiggins732/IdentityServer9.AccessTokenValidation>`_

ASP.NET Core authentication handler for validating tokens in APIs. The handler allows supporting both JWT and reference tokens in the same API.

Templates
^^^^^^^^^
`nuget <https://www.nuget.org/packages/HigginsSoft.IdentityServer9.Templates>`_ | `github <https://github.com/alexhiggins732/IdentityServer9.Templates>`_

Contains templates for the dotnet CLI.

Dev builds
^^^^^^^^^^
In addition we publish CI builds to our package repository.
Add the following ``nuget.config`` to your project::

    <?xml version="1.0" encoding="utf-8"?>
        <configuration>
            <packageSources>
                <clear />
                <add key="IdentityServer CI" value="https://www.myget.org/F/identity/api/v3/index.json" />
            </packageSources>
        </configuration>
