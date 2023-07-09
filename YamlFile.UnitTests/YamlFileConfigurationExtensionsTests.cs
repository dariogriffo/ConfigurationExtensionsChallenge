using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace YamlFile.UnitTests
{
    public class YamlFileConfigurationExtensionsTests
    {
        [Fact]
        public void AddYamlFile_When_Has_Basic_Properties_Values()
        {
            var configuration = new ConfigurationBuilder().AddYamlFile().Build();
            configuration["first_team"].Should().Be("brazil");
        }
    }
}