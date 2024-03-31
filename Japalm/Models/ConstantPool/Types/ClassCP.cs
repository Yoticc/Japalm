namespace Japalm;
public class ClassCP : ConstantPoolEntry
{
    public string Name;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        var index = ReadU2(p);
        var utf8 = new Utf8CP();
        utf8.ParseRaw(pool, index);
            
        Name = utf8.Value;
    }
}