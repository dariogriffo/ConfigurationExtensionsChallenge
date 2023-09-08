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

        [Fact]
        public void AddYamlFile_When_Values_Has_Quotes()
        {
            var configuration = new ConfigurationBuilder().AddYamlFile("quotes.yml", removeWrappingQuotes: true).Build();
            configuration["author"].Should().Be("Leonardo Da Vinci");
        }

        [Fact]
        public void AddYamlFile_When_Has_Comments()
        {
            var configuration = new ConfigurationBuilder().AddYamlFile("comments.yaml").Build();
            configuration.GetChildren().Should().BeEmpty();
        }

        [Fact]
        public void AddYamlFile_When_Has_DuplicateKeys_ThrowsArgumentException()
        {
            var configuration = new ConfigurationBuilder();
            Assert.Throws<ArgumentException>(() => configuration.AddYamlFile("duplicates.yaml").Build());
        }
    }
}