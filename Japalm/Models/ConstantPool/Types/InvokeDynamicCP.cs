namespace Japalm;
public class InvokeDynamicCP : ConstantPoolEntry
{
    public Attribute BootstrapMethodAttributeIndex;
    public string Name;
    public string Descriptor;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        var attributeIndex = ReadU2(p);
        BootstrapMethodAttributeIndex = new();
        fixed (byte* ptr = pool[attributeIndex].info)
            BootstrapMethodAttributeIndex.ParseRaw(pool, ptr);

        var nameAndTypeIndex = ReadU2(p, 2);
        var nameAndType = new NameAndTypeCP();
        nameAndType.ParseRaw(pool, nameAndTypeIndex);
        Name = nameAndType.Name;
        Descriptor = nameAndType.Descriptor;
    }
}