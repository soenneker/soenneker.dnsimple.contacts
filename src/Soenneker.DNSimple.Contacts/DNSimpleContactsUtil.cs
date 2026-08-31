using Microsoft.Extensions.Configuration;
using Soenneker.DNSimple.Contacts.Abstract;
using Soenneker.DNSimple.OpenApiClient;
using Soenneker.DNSimple.OpenApiClient.Item.Contacts;
using Soenneker.DNSimple.OpenApiClient.Item.Contacts.Item;
using Soenneker.DNSimple.OpenApiClient.Models;
using Soenneker.DNSimple.OpenApiClientUtil.Abstract;
using Soenneker.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.ValueTask;
using Soenneker.Extensions.Task;

namespace Soenneker.DNSimple.Contacts;

///<inheritdoc cref="IDNSimpleContactsUtil"/>
public sealed class DNSimpleContactsUtil : IDNSimpleContactsUtil
{
    private readonly IDNSimpleOpenApiClientUtil _clientUtil;
    private readonly int _accountId;

    public DNSimpleContactsUtil(IDNSimpleOpenApiClientUtil clientUtil, IConfiguration configuration)
    {
        _clientUtil = clientUtil;
        _accountId = configuration.GetValueStrict<int>("DNSimple:AccountId");
    }

    public async ValueTask<Contact> Create(Contact contact, CancellationToken cancellationToken = default)
    {
        return await Create(_accountId, contact, cancellationToken).NoSync();
    }

    private async ValueTask<Contact> Create(int accountId, Contact contact, CancellationToken cancellationToken)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        var requestBody = new ContactCreateRequest
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            Address1 = contact.Address1,
            Address2 = contact.Address2,
            City = contact.City,
            StateProvince = contact.StateProvince,
            PostalCode = contact.PostalCode,
            Country = contact.Country,
            Phone = contact.Phone,
            Fax = contact.Fax,
            JobTitle = contact.JobTitle,
            Label = contact.Label,
            OrganizationName = contact.OrganizationName
        };

        CreateContact201Response? response = await client[accountId].Contacts.PostAsync(requestBody, cancellationToken: cancellationToken).NoSync();
        return response?.Data ?? throw new InvalidOperationException("DNSimple returned no contact after creation.");
    }

    public async ValueTask<Contact> Get(int contactId, CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        GetContact200Response? response = await client[_accountId].Contacts[contactId].GetAsync(cancellationToken: cancellationToken).NoSync();

        return response?.Data ?? throw new InvalidOperationException("DNSimple returned no contact for the requested ID.");
    }

    public async ValueTask<Contact> Update(int contactId, Contact contact, CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        var requestBody = new ContactUpdateRequest
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            Address1 = contact.Address1,
            Address2 = contact.Address2,
            City = contact.City,
            StateProvince = contact.StateProvince,
            PostalCode = contact.PostalCode,
            Country = contact.Country,
            Phone = contact.Phone,
            Fax = contact.Fax,
            JobTitle = contact.JobTitle,
            Label = contact.Label,
            OrganizationName = contact.OrganizationName
        };

        UpdateContact200Response? response = await client[_accountId]
                                                   .Contacts[contactId]
                                                   .PatchAsync(requestBody, cancellationToken: cancellationToken)
                                                   .NoSync();
        return response?.Data ?? throw new InvalidOperationException("DNSimple returned no contact after the update.");
    }

    public async ValueTask Delete(int accountId, int contactId, CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        await client[accountId].Contacts[contactId].DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<Contact[]> List(CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        ListContacts200Response? response = await client[_accountId].Contacts.GetAsync(cancellationToken: cancellationToken).NoSync();
        return response?.Data?.ToArray() ?? [];
    }

    public async ValueTask<Contact> CreateBasic(int accountId, string firstName, string lastName, string email, string address1, string city,
        string stateProvince, string postalCode, string country, string phone, CancellationToken cancellationToken = default)
    {
        var contact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Address1 = address1,
            City = city,
            StateProvince = stateProvince,
            PostalCode = postalCode,
            Country = country,
            Phone = phone
        };

        return await Create(accountId, contact, cancellationToken).NoSync();
    }
}
