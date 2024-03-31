namespace Japalm;
public class Utf8CP : ConstantPoolEntry
{
    public string Value;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        var length = ReadU2(p);
        var bytes = Read(p, length, 2);
        Value = Encoding.UTF8.GetString(bytes);
    }

    public override string ToString() => $"\"{Value}\"";
}