using Microsoft.Extensions.Configuration;

namespace YamlFile;
public class YamlFileConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly string _fileName;
    private readonly bool _trim;
    private readonly bool _removeWrappingQuotes;
    private readonly FileSystemWatcher _watcher;

    public YamlFileConfigurationProvider(string fileName, bool trim, bool removeWrappingQuotes, bool reloadOnChange)
    {
        _fileName = fileName;
        _trim = trim;
        _removeWrappingQuotes = removeWrappingQuotes;

        _watcher = new FileSystemWatcher(Path.GetDirectoryName(_fileName) ?? ".");
        _watcher.Changed += new FileSystemEventHandler(OnChanged);
        _watcher.EnableRaisingEvents = true;
    }

    private void OnChanged(object sender, FileSystemEventArgs e) {
        Load();
        OnReload();
    }

    public override void Load() {
        try
        {
            LoadData();
        } 
        catch(FormatException ex)
        {
            throw;
        }
    }

    private void LoadData()
    {
        var linesWithPropertyValues = 
            File
                .ReadAllLines(_fileName)
                .Where(l => !l.StartsWith("#"));
        
        var configuration = 
            linesWithPropertyValues
                .Select(ParseQuotes)
                .Select(l => l.Split(":"))
                .Select(l => new KeyValuePair<string, string>(l.First(), l.LastOrDefault("").Trim()));

        string ParseQuotes(string l)
        {
            if(!_removeWrappingQuotes) 
            {
                return l;
            }

            var parts = l.Split(':');
            l = string.Join(":", parts.Skip(1));
            return $"{parts[0]}:{l.TrimStart().Trim('\"')}";
        }

        Data = configuration.ToDictionary(l => l.Key, l => l.Value);
    }

    public void Dispose()
    {
        _watcher?.Dispose();
    }
}
