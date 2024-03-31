namespace Japalm;
public class FloatCP : ConstantPoolEntry
{
    public float Value;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        Value = ToFloat(ReadU4(p));
    }
}