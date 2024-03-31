namespace Japalm;
public class JarFile : JarEntry
{
    public JarFile(string path, byte[] bytes) : base(path) => Bytes = bytes;
    public JarFile(string path) : this(path, []) { }

    public byte[] Bytes;

    public override byte[] GetBytes() => Bytes;

    public static JarFile FromFile(string entryPath, string filePath) => new(entryPath, File.ReadAllBytes(filePath));
}