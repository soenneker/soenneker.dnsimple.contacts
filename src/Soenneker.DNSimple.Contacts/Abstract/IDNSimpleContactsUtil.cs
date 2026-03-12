using Soenneker.DNSimple.OpenApiClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.DNSimple.Contacts.Abstract;

/// <summary>
/// Interface for a utility class that manages DNSimple contacts
/// </summary>
public interface IDNSimpleContactsUtil
{
    /// <summary>
    /// Creates a DNSimple contact.
    /// </summary>
    /// <param name="contact">The contact to create.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created contact.</returns>
    ValueTask<Contact> Create(Contact contact, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a DNSimple contact by ID.
    /// </summary>
    /// <param name="contactId">The ID of the contact to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The retrieved contact.</returns>
    ValueTask<Contact> Get(int contactId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a DNSimple contact.
    /// </summary>
    /// <param name="contactId">The ID of the contact to update.</param>
    /// <param name="contact">The updated contact values.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The updated contact.</returns>
    ValueTask<Contact> Update(int contactId, Contact contact, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a DNSimple contact.
    /// </summary>
    /// <param name="accountId">The DNSimple account ID.</param>
    /// <param name="contactId">The ID of the contact to delete.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    ValueTask Delete(int accountId, int contactId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all DNSimple contacts for the configured account.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An array of contacts.</returns>
    ValueTask<Contact[]> List(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a basic DNSimple contact with common fields.
    /// </summary>
    /// <param name="accountId">The DNSimple account ID.</param>
    /// <param name="firstName">First name of the contact.</param>
    /// <param name="lastName">Last name of the contact.</param>
    /// <param name="email">Email of the contact.</param>
    /// <param name="address1">Address line 1 of the contact.</param>
    /// <param name="city">City of the contact.</param>
    /// <param name="stateProvince">State or province of the contact.</param>
    /// <param name="postalCode">Postal code of the contact.</param>
    /// <param name="country">Country of the contact.</param>
    /// <param name="phone">Phone number of the contact.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The created contact.</returns>
    ValueTask<Contact> CreateBasic(int accountId, string firstName, string lastName, string email, string address1, string city, string stateProvince,
        string postalCode, string country, string phone, CancellationToken cancellationToken = default);
}