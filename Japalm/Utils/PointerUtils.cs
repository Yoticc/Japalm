namespace Japalm;
public static unsafe class PointerUtils
{
    public static u1 ReadU1(u1* ptr, int offset = 0) => *(ptr + offset);
    public static u2 ReadU2(u1* ptr, int offset = 0) => SwapU2(*(u2*)(ptr + offset));
    public static u4 ReadU4(u1* ptr, int offset = 0) => SwapU4(*(u4*)(ptr + offset));
    public static u8 ReadU8(u1* ptr, int offset = 0) => SwapU8(*(u8*)(ptr + offset));
    public static u1[] Read(u1* ptr, int len, int offset = 0) => MemEx.Read(ptr + offset, len);

    public static u2 SwapU2(u2 val) => (u2)(val << 8 & 0xFF00 | val >> 8 & 0x00FFF);
    public static u4 SwapU4(u4 val) => val << 24 & 0xFF000000 | val << 8 & 0x00FF0000 | val >> 8 & 0x0000FF00 | val >> 24 & 0x000000FF;
    public static u8 SwapU8(u8 val) => (val >> 56) | ((val << 40) & 0x00FF000000000000) | ((val << 24) & 0x0000FF0000000000) | ((val << 8) & 0x000000FF00000000) | ((val >> 8) & 0x00000000FF000000) | ((val >> 24) & 0x0000000000FF0000) | ((val >> 40) & 0x000000000000FF00) | (val << 56);

    public static float ToFloat(u4 val) => *(float*)&val;
    public static double ToDouble(u8 val) => *(double*)&val;
}