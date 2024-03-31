namespace Japalm;
public class MethodRefCP : ConstantPoolEntry
{
    public string Class;
    public string Name;
    public string Descriptor;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        var classIndex = ReadU2(p);
        var @class = new ClassCP();
        @class.ParseRaw(pool, classIndex);
        Class = @class.Name;

        var nameAndTypeIndex = ReadU2(p, 2);
        var nameAndType = new NameAndTypeCP();
        nameAndType.ParseRaw(pool, nameAndTypeIndex);
        Name = nameAndType.Name;
        Descriptor = nameAndType.Descriptor;
    }
}