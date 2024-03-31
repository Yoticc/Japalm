namespace Japalm;
public class MethodTypeCP : ConstantPoolEntry
{
    public string Descriptor;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        var index = ReadU2(p);
        var utf8 = new Utf8CP();
        utf8.ParseRaw(pool, index);

        Descriptor = utf8.Value;
    }
}