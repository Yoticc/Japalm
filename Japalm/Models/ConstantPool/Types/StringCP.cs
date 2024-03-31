namespace Japalm;
public class StringCP : ConstantPoolEntry
{
    public string Value;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        var index = ReadU2(p);
        var utf8 = new Utf8CP();
        utf8.ParseRaw(pool, index);

        Value = utf8.Value;
    }

    public override string ToString() => $"\"{Value}\"";
}