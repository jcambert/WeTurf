using System.Reflection.PortableExecutable;

namespace We.Csv.Tests
{
    public class TestReader
    {
        [Fact]
        public async Task Test1()
        {
            var reader = new Reader<TestCsv>("predicted.csv", true, ';');
            Assert.NotNull(reader);
            reader.OnReadLine.Subscribe(o =>
            {
                Console.WriteLine(o);
            });

            await reader.Start();
        }
    }
}