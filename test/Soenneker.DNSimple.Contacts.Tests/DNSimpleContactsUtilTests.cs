using Soenneker.DNSimple.Contacts.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.DNSimple.Contacts.Tests;

[Collection("Collection")]
public sealed class DNSimpleContactsUtilTests : FixturedUnitTest
{
    private readonly IDNSimpleContactsUtil _util;

    public DNSimpleContactsUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IDNSimpleContactsUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
