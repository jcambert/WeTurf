using Microsoft.Extensions.DependencyInjection;
using System;
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
            services.UsePythonExecutor(
                o =>
                {
                    o.UseAnaconda = useAnaconda;
                }
            );
            using ServiceProvider sp = services.BuildServiceProvider();
            var pyexe = sp.GetService<IPythonExecutor>();
            Assert.NotNull(pyexe);
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
        public async Task TestPythonWithoutAnacondaActivationAndOnlineCommand(
            bool inConsole,
            bool useReactiveOutput,
            string msg
        )
        {
            IServiceCollection services = new ServiceCollection();
            services.UsePythonExecutor(
                o =>
                {
                    o.ExecuteInConsole = inConsole;
                    o.UseAnaconda = false;
                    o.UseReactiveOutput = useReactiveOutput;
                    o.PythonPath = "E:\\anaconda\\";
                }
            );
            using ServiceProvider sp = services.BuildServiceProvider();
            var pyexe = sp.GetRequiredService<IPythonExecutor>();
            if (useReactiveOutput)
                pyexe.OnOutput.Subscribe(
                    x =>
                    {
                        output.WriteLine(x);
                    }
                );
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

                string value = ((Valid<string>)result).Value;
                Assert.NotNull(value);
                output.WriteLine(value);
            }
        }

        private class SayHelloCommand : BaseCommand
        {
            public override string GetCommand()
            {
                return "dir";
            }
        }

        [Theory]
        [InlineData(true, false, "print('Hi')")]
        [InlineData(false, false, "print('Hi')")]
        [InlineData(true, false, "test.py toto")]
        public async Task TestAnacondaActivation(bool inConsole, bool useReactiveOutput, string msg)
        {
            IServiceCollection services = new ServiceCollection();
            services.UsePythonExecutor(
                o =>
                {
                    o.ExecuteInConsole = inConsole;
                    o.UseAnaconda = true;
                    o.UseReactiveOutput = useReactiveOutput;
                    o.AnacondBasePath = "E:\\anaconda\\";
                    o.EnvironmentName = "base";
                }
            );
            using ServiceProvider sp = services.BuildServiceProvider();
            var exe = sp.GetService<IPythonExecutor>();
            Assert.NotNull(exe);
            var result = await exe.SendAsync(msg);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            if (useReactiveOutput)
            {
                Assert.True(result.GetType().Equals(typeof(Valid)));
            }
            else
            {
                Assert.True(result.GetType().Equals(typeof(Valid<string>)));

                var value = ((Valid<string>)result).Value;
                Assert.NotNull(value);
                output.WriteLine(value);
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
                .UseCommandExecutor(
                    opt =>
                    {
                        opt.ExecuteInConsole = inConsole;
                        opt.UseReactiveOutput = useReactiveOutput;
                    }
                )
                .AddTransient<ICommand, SayHelloCommand>()
                .AddTransient<ICommand, ClearCommand>();
            using ServiceProvider sp = services.BuildServiceProvider();
            var exe = sp.GetService<ICommandExecutor>();
            Assert.NotNull(exe);
            if (useReactiveOutput)
                exe.OnOutput.Subscribe(
                    x =>
                    {
                        output.WriteLine(x);
                    }
                );
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

                var value = ((Valid<string>)result).Value;
                Assert.NotNull(value);
                output.WriteLine(value);
            }
        }

        [Theory]
        [InlineData("scrap.py", "01042023", "02042023")]
        public async Task TestScrap(string script, string start, string end)
        {
            IServiceCollection services = new ServiceCollection();
            services.UsePythonExecutor(
                o =>
                {
                    o.UseAnaconda = true;
                    o.UseReactiveOutput = true;
                    o.PythonPath = @"E:\anaconda\";
                    o.WorkingDirectory = @"E:\projets\pmu_scrapper";
                }
            );
            using ServiceProvider sp = services.BuildServiceProvider();
            var exe = sp.GetService<IPythonExecutor>();
            Assert.NotNull(exe);

            exe.OnOutput.Subscribe(
                x =>
                {
                    output.WriteLine(x);
                }
            );
            var msg = $"{script} start={start} end={end}";
            var result = await exe.SendAsync(msg);
        }

        [Theory]
        [InlineData("-c", "print('Hi')")]
        public void RunPythonScript(string cmd, string args)
        {
            ProcessStartInfo start =
                new()
                {
                    FileName = "E:\\anaconda\\python.exe",
                    Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args),
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };
            using Process? process = Process.Start(start);
            using StreamReader? reader = process?.StandardOutput;
            if (reader is not null)
            {
                string result = reader.ReadToEnd();
                output.WriteLine($"{result}");
                Assert.False(string.IsNullOrEmpty(result));
                
            }
            Assert.True(reader != null);
        }
    }
}
