namespace Japalm;
public class NameAndTypeCP : ConstantPoolEntry
{
    public string Name;
    public string Descriptor;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        var nameIndex = ReadU2(p);
        var nameUtf8 = new Utf8CP();
        nameUtf8.ParseRaw(pool, nameIndex);

        var descriptorIndex = ReadU2(p + 2);
        var descriptorUtf8 = new Utf8CP();
        descriptorUtf8.ParseRaw(pool, descriptorIndex);

        Name = nameUtf8.Value;
        Descriptor = descriptorUtf8.Value;
    }
}