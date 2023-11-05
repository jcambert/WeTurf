using Xunit.Abstractions;

namespace We.Results.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;
        public UnitTest1(ITestOutputHelper outputWriter)
        {
            this._output = outputWriter;
        }
        [Theory]
        [InlineData(0, 1)]
        public void TestEnsureResult(int v, int r)
        {
            Result<int> result = v;
            var res = result.Ensure(x => x == r, new Error($"Result {v} != than {r}"));
            _output.WriteLine(res.Errors.AsString());
            Assert.False(res);
        }

        [Theory]
        [InlineData(0, 1)]
        public async Task TestMatchSimpleSuccessResult(int vrai, int faux)
        {
            Func<Task<Result>> result=()=>Task.FromResult( Result.Success());

            var res = await result().Match(
                ()=> {
                    return vrai;
                },
                failure =>
                {
                    return faux;
                });
            
            Assert.Equal(res,vrai);
        }

        [Theory]
        [InlineData(0, 1)]
        public async Task TestMatchSimpleFailureResult(int vrai, int faux)
        {
            Func<Task<Result>> result = () => Task.FromResult(Result.Failure(new Error("toto")));

            var res = await result().Match(
                () => {
                    return vrai;
                },
                failure =>
                {
                    return faux;
                });

            Assert.Equal(res, faux);
        }

        [Fact]
        public void TestReactiveResult()
        {
            /*Result<int> result = Result.Create(10);
            ReactiveResult reactiveResult = Result.CreateReactive();
            reactiveResult.OnSuccess.Subscribe(_output.WriteLine("Success"));
            reactiveResult.OnFailure.Subscribe(_output.WriteLine("Failure"));*/
            ReactiveResult<int> result = new ReactiveResult<int>();
            result.Match(
                value => _output.WriteLine($"Succes 0:{value}"),
                errors => _output.WriteLine($"Error 0:{errors.AsString()}"),
                ()=>_output.WriteLine("The result is completed")
                );

            /*result.Match(
                value => _output.WriteLine($"Succes 1:{value}"),
                errors => _output.WriteLine($"Error 1:{errors.AsString()}"));
                result.Fail(new ApplicationException("RIEN"));
            */

            result.Ok(15);result.Reset();
            result.Complete();
            result.Ok(10); result.Reset();
            result.Fail(new ApplicationException("RIEN"));
            result.Reset();
            result.Ok(5);

            
        }
    }
}
