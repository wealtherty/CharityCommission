using FluentAssertions;
using Xunit;

namespace CharityCommission.IntegrationTests;

public class CharityCommissionClientTests : IClassFixture<Fixture>
{
    private readonly Fixture _fixture;

    public CharityCommissionClientTests(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(Skip = Fixture.ReasonToSkip)]
    public async Task Can_get_Charity()
    {
        const string number = "235351";
        
        var charity = await _fixture.GetClient().GetCharityAsync(number);

        charity.Should().NotBeNull();
        charity.Number.Should().Be(number);
    }
}