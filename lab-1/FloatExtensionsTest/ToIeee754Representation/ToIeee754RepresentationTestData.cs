namespace FloatExtensionsTest.ToIeee754Representation;

public class ToIeee754RepresentationTestData : TestDataBase<ToIeee754RepresentationTestCase>
{
    protected override IEnumerable<ToIeee754RepresentationTestCase> GetTestData()
    {
        yield return new ToIeee754RepresentationTestCase()
        {
            Description = "Test for 42.5 float representation",
            Number = 42.5f,
            ExpectedResult = "01000010001010100000000000000000",
        };
        yield return new ToIeee754RepresentationTestCase()
        {
            Description = "Test for -42.5 float representation",
            Number = -42.5f,
            ExpectedResult = "11000010001010100000000000000000",
        };
    }
}