using DotNet.Testcontainers.Containers;
using Testcontainers.MsSql;

namespace SipCartTesting.Setup
{
    public class ContainerizedTests
    {
        protected MsSqlContainer? _msSqlContainer;

        protected async Task<MsSqlContainer> CreateContainerAsync()
        {
            string command = await ReadTestDataFromFileAsync();
            MsSqlContainer container = await InitContainerAsync(command);
            //Create the container
            _msSqlContainer = container;
            return container;
        }

        private static async Task<string> ReadTestDataFromFileAsync()
        {
            //Read the test data from file
            string filePath = Directory.GetCurrentDirectory() + "/Scripts/DbScript.sql";
            Assert.That(File.Exists(filePath));
            string fileContent = await File.ReadAllTextAsync(filePath);
            Assert.That(fileContent, Is.Not.Null);
            return fileContent;
        }

        private async Task<MsSqlContainer> InitContainerAsync(string command)
        {
            _msSqlContainer = new MsSqlBuilder()
                   .Build();
            await _msSqlContainer.StartAsync();
            ExecResult execResult = await _msSqlContainer.ExecScriptAsync(command);
            Assert.That(execResult.ExitCode, Is.EqualTo(0));    //check if SQL script worked
            return _msSqlContainer;
        }

        protected async Task DisposeContainerAsync()
        {
            if (_msSqlContainer != null)
            {
                await _msSqlContainer.StopAsync();
                await _msSqlContainer.DisposeAsync();
            }
        }

    }
}