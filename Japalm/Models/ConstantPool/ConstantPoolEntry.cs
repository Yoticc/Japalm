namespace Japalm;
public abstract unsafe class ConstantPoolEntry 
{
    internal ConstantPoolEntry ParseRaw(cp_info[] pool, int index) => ParseRaw(pool, pool[index]);
    internal ConstantPoolEntry ParseRaw(cp_info[] pool, cp_info cp) => ParseRaw(pool, cp.info);
    internal ConstantPoolEntry ParseRaw(cp_info[] pool, byte[] arr)
    {
        fixed (byte* p = arr)
            ParseRaw(pool, p);
        return this;
    }
    internal abstract void ParseRaw(cp_info[] pool, byte* p);
}