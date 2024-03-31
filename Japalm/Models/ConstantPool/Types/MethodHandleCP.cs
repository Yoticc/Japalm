namespace Japalm;
public class MethodHandleCP : ConstantPoolEntry
{
    public static readonly MethodHandleKind[] FieldKinds =
    [
        MethodHandleKind.GetField,
        MethodHandleKind.GetStatic,
        MethodHandleKind.PutField,
        MethodHandleKind.PutStatic
    ];

    public static readonly MethodHandleKind[] MethodKinds =
    [
        MethodHandleKind.InvokeVirtual,
        MethodHandleKind.InvokeStatic,
        MethodHandleKind.InvokeInterface
    ];

    public MethodHandleKind Kind;
    public string Class;
    public string Name;
    public string Descriptor;

    internal override unsafe void ParseRaw(cp_info[] pool, byte* p)
    {
        Kind = (MethodHandleKind)(*p);

        var index = ReadU2(p, 1);
        var field = new FieldRefCP();
        field.ParseRaw(pool, pool[index]);

        Class = field.Class;
        Name = field.Name;
        Descriptor = field.Descriptor;
    }
}