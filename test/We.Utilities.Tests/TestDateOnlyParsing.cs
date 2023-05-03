namespace We.Utilities.Tests
{
    public class TestDateOnlyParsing
    {
        [Theory]
        [InlineData("17/02/2023")]
        [InlineData("17022023")]
        [InlineData("170223")]
        [InlineData("17-02-2023")]
        [InlineData("17/02/23")]
        public void Test1(string v)
        {
            Assert.True(v.TryParseToDateOnly(out var res));
            Assert.True(res == new DateOnly(2023, 2, 17));
        }
    }
}
