namespace Japalm;
public class LongCP : ConstantPoolEntry
{
    public long Value;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        Value = (long)ReadU8(p);
    }
}