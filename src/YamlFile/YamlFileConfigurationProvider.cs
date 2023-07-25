using Microsoft.Extensions.Configuration;

namespace YamlFile;
public class YamlFileConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly string _fileName;
    private readonly bool _trim;
    private readonly bool _removeWrappingQuotes;
    private readonly string _prefix;
    private readonly FileSystemWatcher _watcher;

    public YamlFileConfigurationProvider(string fileName, bool trim, bool removeWrappingQuotes, string prefix, bool reloadOnChange)
    {
        _fileName = fileName;
        _trim = trim;
        _removeWrappingQuotes = removeWrappingQuotes;
        _prefix = prefix;

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
                .Select(l => l.Split(":"))
                .Select(l => new KeyValuePair<string, string>(l[0], l[1].TrimStart()));
                
        Data = configuration.ToDictionary(l => l.Key, l => l.Value);
    }

    public void Dispose()
    {
        _watcher?.Dispose();
    }
}
