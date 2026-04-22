using Soenneker.DNSimple.Contacts.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.DNSimple.Contacts.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class DNSimpleContactsUtilTests : HostedUnitTest
{
    private readonly IDNSimpleContactsUtil _util;

    public DNSimpleContactsUtilTests(Host host) : base(host)
    {
        _util = Resolve<IDNSimpleContactsUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
