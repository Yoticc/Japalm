using System.IO.Compression;

namespace Japalm;
public sealed class Jar
{
    public readonly List<JarClass> Classes = [];
    public readonly List<JarFile> Files = [];

    public void Save(string path)
    {

    }

    public static Jar Open(string path)
    {
        var jar = new Jar();

        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var archive = new ZipArchive(fs);
        
        foreach (var entry in archive.Entries)
        {
            var name = entry.FullName;
            var isClass = name.EndsWith(".class");
            var bytes = ExtractZipEntry(entry);
            if (isClass)
            {
                var cls = ClassParser.Parse(bytes);
                var jarClass = new JarClass(name, cls);
                jar.Classes.Add(jarClass);
            }
            else
            {
                var jarFile = new JarFile(name, bytes);
                jar.Files.Add(jarFile);
            }
        }

        return jar;
    }

    public static Jar Create() => new();

    static byte[] ExtractZipEntry(ZipArchiveEntry entry)
    {
        using var stream = entry.Open();
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}