[![](https://img.shields.io/nuget/v/soenneker.dnsimple.contacts.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dnsimple.contacts/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dnsimple.contacts/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dnsimple.contacts/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dnsimple.contacts.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dnsimple.contacts/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dnsimple.contacts/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dnsimple.contacts/actions/workflows/codeql.yml)

# Soenneker.DNSimple.Contacts

Provides focused operations for creating, retrieving, updating, listing, and deleting DNSimple contacts.

## Installation

```bash
dotnet add package Soenneker.DNSimple.Contacts
```

## Configuration and registration

```json
{
  "DNSimple": {
    "AccountId": 12345,
    "Token": "your-api-token",
    "Test": false
  }
}
```

```csharp
using Soenneker.DNSimple.Contacts.Registrars;

services.AddDNSimpleContactsUtilAsScoped();
```

The configured account is used by `Create`, `Get`, `Update`, and `List`. `Delete` and `CreateBasic` accept an explicit account ID.

## Usage

```csharp
using Soenneker.DNSimple.Contacts.Abstract;
using Soenneker.DNSimple.OpenApiClient.Models;

public sealed class ContactService(IDNSimpleContactsUtil contacts)
{
    public ValueTask<Contact> Create(Contact contact, CancellationToken cancellationToken)
    {
        return contacts.Create(contact, cancellationToken);
    }

    public ValueTask<Contact[]> List(CancellationToken cancellationToken)
    {
        return contacts.List(cancellationToken);
    }
}
```

`List` returns the contacts in the API response. The generated request builder currently exposes sorting but not page controls, so this wrapper does not promise automatic traversal of additional pages.
