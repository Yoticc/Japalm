namespace Japalm;
public abstract class JarEntry
{
    public JarEntry(string path)
    {
        Path = path;
    }

    public string Path;
    public abstract byte[] GetBytes();

    public string Name
    {
        get => Path.Split('/')[^1];
        set
        {
            var splitted = Path.Split('/');
            splitted[^1] = value;
            Path = string.Join('/', splitted);
        }
    }
}