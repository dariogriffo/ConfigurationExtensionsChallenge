using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace YamlFile.UnitTests
{
    public class YamlFileConfigurationExtensionsTests
    {
        [Fact]
        public void AddYamlFile_When_Has_One_Property_Value()
        {
            var configuration = new ConfigurationBuilder().AddYamlFile().Build();
            configuration["first_team"].Should().Be("brazil");
        }

        [Fact]
        public void AddYamlFile_When_Has_MoreThanOne_Properties_Values()
        {
            var configuration = new ConfigurationBuilder().AddYamlFile().Build();
            configuration["second_team"].Should().Be("germany");
        }
    }
}