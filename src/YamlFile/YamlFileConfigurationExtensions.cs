using Microsoft.Extensions.Configuration;

namespace YamlFile;

public static class YamlFileConfigurationExtensions
{
    public static IConfigurationBuilder AddYamlFile(
        this IConfigurationBuilder builder,
        string fileName = "football.yml",
        bool trim = false,
        bool removeWrappingQuotes = true,
        string prefix = null,
        bool reloadOnChange = false) 
    {
        var filePath = fileName;
        if(!Path.IsPathRooted(fileName))
        {
            var directory = builder.Properties.TryGetValue("FileProvider", out var p) && p is FileConfigurationProvider configurationProvider
                ? Path.GetDirectoryName(configurationProvider.Source.Path)
                : Directory.GetCurrentDirectory();

            filePath = Path.Combine(directory, fileName);
        }

        if(!File.Exists(filePath))
        {
            return builder;
        }

        var provider = new YamlFileConfigurationProvider(filePath, trim, removeWrappingQuotes, prefix, reloadOnChange);
        return builder.Add(new YamlFileConfigurationSource(provider));
    }
}
