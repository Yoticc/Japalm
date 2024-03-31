namespace Japalm;
public class DoubleCP : ConstantPoolEntry
{
    public double Value;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        Value = ToDouble(ReadU8(p));
    }
}