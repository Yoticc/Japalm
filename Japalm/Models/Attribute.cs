namespace Japalm;
public unsafe class Attribute
{
    public string Name;
    public byte[] Bytes;

    internal void ParseRaw(cp_info[] pool, byte* p)
    {
        var nameIndex = ReadU2(p);
        var nameUtf8 = new Utf8CP();
        nameUtf8.ParseRaw(pool, nameIndex);
        Name = nameUtf8.Value;

        var length = ReadU4(p, 4);
        Bytes = MemEx.Read(p + 8, (int)length);
    }
}