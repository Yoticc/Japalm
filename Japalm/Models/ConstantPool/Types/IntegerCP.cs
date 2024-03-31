namespace Japalm;
public class IntegerCP : ConstantPoolEntry
{
    public int Value;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        Value = (int)ReadU4(p);
    }
}