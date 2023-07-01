using Microsoft.Extensions.Configuration;

namespace YamlFile
{
    public class YamlFileConfigurationSource : IConfigurationSource
    {
        private readonly YamlFileConfigurationProvider _provider;

        public YamlFileConfigurationSource(YamlFileConfigurationProvider provider)
        {
            _provider = provider;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return _provider;
        }
    }
}