namespace Japalm;
public class JarClass : JarEntry
{
    public JarClass(string path, Class cls) : base(path)
    {
        Class = cls;
    }

    public Class Class;

    public override byte[] GetBytes()
    {
        throw new NotImplementedException();
    }
}