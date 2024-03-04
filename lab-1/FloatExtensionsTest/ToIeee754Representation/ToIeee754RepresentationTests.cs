using FluentAssertions;
using lab_1;

namespace FloatExtensionsTest.ToIeee754Representation;

public class ToIeee754RepresentationTests
{
    [Theory]
    [ClassData(typeof(ToIeee754RepresentationTestData))]
    public void ToIeee754Representation(ToIeee754RepresentationTestCase testCase)
    {
        var expectedResult = testCase.ExpectedResult;

        var actualResult = testCase.Number.ToIeee754Representation();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [ClassData(typeof(ToIeee754RepresentationTestData))]
    public void ToIeee754RepresentationLinq(ToIeee754RepresentationTestCase testCase)
    {
        var expectedResult = testCase.ExpectedResult;

        var actualResult = testCase.Number.ToIeee754RepresentationLinq();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }
}