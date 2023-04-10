using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using We.Results;
using Xunit.Abstractions;

namespace We.Processes.Tests
{

    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestPythonExecutorRegistered(bool useAnaconda)
        {
            IServiceCollection services = new ServiceCollection();
            services.UsePythonExecutor(o =>
            {
                o.UseAnaconda = useAnaconda;
            });
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                var pyexe = sp.GetService<IPythonExecutor>();
                Assert.NotNull(pyexe);

            }
        }
        [Theory]
        [InlineData(true, false, "print('Hi')")]
        [InlineData(false, false, "print('Hi')")]
        [InlineData(true, true, "print('Hi')")]
        [InlineData(false, true, "print('Hi')")]
        [InlineData(true, false, "test.py toto")]
        [InlineData(false, false, "test.py toto")]
        [InlineData(true, true, "test.py toto")]
        [InlineData(false, true, "test.py toto")]
        public async Task TestPythonWithoutAnacondaActivationAndOnlineCommand(bool inConsole, bool useReactiveOutput, string msg)
        {
            IServiceCollection services = new ServiceCollection();
            services.UsePythonExecutor(o =>
            {
                o.ExecuteInConsole = inConsole;
                o.UseAnaconda = false;
                o.UseReactiveOutput = useReactiveOutput;
                o.PythonPath = "E:\\anaconda\\";
            });
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                var pyexe = sp.GetService<IPythonExecutor>();
                if (useReactiveOutput)
                    pyexe.OnOutput.Subscribe(x =>
                    {
                        output.WriteLine(x);
                    });
                Assert.NotNull(pyexe);
                var result = await pyexe.SendAsync(msg);
                Assert.NotNull(result);
                Assert.True(result.IsSuccess);
                if (useReactiveOutput)
                {
                    Assert.True(result.GetType().Equals(typeof(Valid)));
                }
                else
                {
                    Assert.True(result.GetType().Equals(typeof(Valid<string>)));

                    var value = (result as Valid<string>).Value;
                    Assert.NotNull(value);
                    output.WriteLine(value);
                }

            }
        }

        private class SayHelloCommand : BaseCommand
        {
            public override string GetCommand()
            {
                return "dir";
            }
        }
        private class ClearCommand : BaseCommand
        {
            public override string GetCommand()
            {
                return "cls";
            }
        }
        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public async Task TestBasicCommand(bool inConsole, bool useReactiveOutput)
        {
            IServiceCollection services = new ServiceCollection();
            services
                .UseExecutor()
                .UseCommandExecutor(opt =>
                {
                    opt.ExecuteInConsole = inConsole;
                    opt.UseReactiveOutput = useReactiveOutput;
                })
                .AddTransient<ICommand, SayHelloCommand>()
                .AddTransient<ICommand, ClearCommand>(); 
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                var exe = sp.GetService<ICommandExecutor>();
                Assert.NotNull(exe);
                if (useReactiveOutput)
                    exe.OnOutput.Subscribe(x =>
                    {
                        output.WriteLine(x);
                    });
                var result = await exe.Execute();
                Assert.NotNull(result);
                Assert.True(result.IsSuccess);
                if (useReactiveOutput)
                {
                    Assert.True(result.GetType().Equals(typeof(Valid)));
                }
                else
                {
                    Assert.True(result.GetType().Equals(typeof(Valid<string>)));

                    var value = (result as Valid<string>).Value;
                    Assert.NotNull(value);
                    output.WriteLine(value);
                }
            }
        }

        [Theory]
        [InlineData("-c", "print('Hi')")]
        public string RunPythonScript(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "E:\\anaconda\\python.exe";
            start.Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    output.WriteLine($"{result}");
                    return result;
                }
            }
        }
    }
}