using System.Reflection.PortableExecutable;
using Xunit.Abstractions;

namespace We.Csv.Tests
{
    public class TestReader
    {
        private readonly ITestOutputHelper output;
        public TestReader(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Theory()]
        //[InlineData(@"E:\projets\pmu_scrapper\output\predicted",101)]
        [InlineData("predicted-light",2)]
        //[InlineData("predicted",185)]
        public async Task Test1(string filename,int attendee)
        {
            int index = 0;
            var reader = new Reader<Predicted>($"{filename}.csv", true, ';');
            Assert.NotNull(reader);
            reader.OnReadLine.Subscribe(o =>
            {
                output.WriteLine($"{o.Index} / {o.Value.ToString()}");
                index++;
            }, () =>
            {
                output.WriteLine("End of stream");
            });

            await reader.Start();
            Assert.True(index==attendee);
        }
    }
}