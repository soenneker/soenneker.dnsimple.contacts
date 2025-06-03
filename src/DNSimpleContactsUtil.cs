using Microsoft.Extensions.Configuration;
using Soenneker.DNSimple.Contacts.Abstract;
using Soenneker.DNSimple.OpenApiClient;
using Soenneker.DNSimple.OpenApiClient.Item.Contacts;
using Soenneker.DNSimple.OpenApiClient.Item.Contacts.Item;
using Soenneker.DNSimple.OpenApiClient.Models;
using Soenneker.DNSimple.OpenApiClientUtil.Abstract;
using Soenneker.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.ValueTask;
using Soenneker.Extensions.Task;

namespace Soenneker.DNSimple.Contacts;

/// <summary>
/// Utility class for managing DNSimple contacts
/// </summary>
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
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        var requestBody = new ContactsPostRequestBody
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

        ContactsPostResponse? response =
            await client[_accountId].Contacts.PostAsContactsPostResponseAsync(requestBody, cancellationToken: cancellationToken).NoSync();
        return response.Data;
    }

    public async ValueTask<Contact> Get(int contactId, CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        WithContactGetResponse? response =
            await client[_accountId].Contacts[contactId].GetAsWithContactGetResponseAsync(cancellationToken: cancellationToken).NoSync();
        return response.Data;
    }

    public async ValueTask<Contact> Update(int contactId, Contact contact, CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        var requestBody = new WithContactPatchRequestBody
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

        WithContactPatchResponse? response = await client[_accountId]
                                                   .Contacts[contactId]
                                                   .PatchAsWithContactPatchResponseAsync(requestBody, cancellationToken: cancellationToken)
                                                   .NoSync();
        return response.Data;
    }

    public async ValueTask Delete(int accountId, int contactId, CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        await client[accountId].Contacts[contactId].DeleteAsync(cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask<Contact[]> List(CancellationToken cancellationToken = default)
    {
        DNSimpleOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();

        ContactsGetResponse? response = await client[_accountId].Contacts.GetAsContactsGetResponseAsync(cancellationToken: cancellationToken).NoSync();
        return response.Data?.ToArray() ?? [];
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

        return await Create(contact, cancellationToken);
    }
}