namespace Japalm;
public class Field
{
    public FieldAccessFlags Access;
    public string Name;
    public string Descriptor;
    public List<Attribute> Attributes;

    public bool IsPublic { get => GetAccessFlag(FieldAccessFlags.Public); set => SetAccessFlag(FieldAccessFlags.Public, value); }
    public bool IsPrivate { get => GetAccessFlag(FieldAccessFlags.Private); set => SetAccessFlag(FieldAccessFlags.Private, value); }
    public bool IsProtected { get => GetAccessFlag(FieldAccessFlags.Protected); set => SetAccessFlag(FieldAccessFlags.Protected, value); }
    public bool IsStatic { get => GetAccessFlag(FieldAccessFlags.Static); set => SetAccessFlag(FieldAccessFlags.Static, value); }
    public bool IsFinal { get => GetAccessFlag(FieldAccessFlags.Final); set => SetAccessFlag(FieldAccessFlags.Final, value); }
    public bool IsVolatile { get => GetAccessFlag(FieldAccessFlags.Volatile); set => SetAccessFlag(FieldAccessFlags.Volatile, value); }
    public bool IsTransient { get => GetAccessFlag(FieldAccessFlags.Transient); set => SetAccessFlag(FieldAccessFlags.Transient, value); }
    public bool IsSynthetic { get => GetAccessFlag(FieldAccessFlags.Synthetic); set => SetAccessFlag(FieldAccessFlags.Synthetic, value); }
    public bool IsEnum { get => GetAccessFlag(FieldAccessFlags.Enum); set => SetAccessFlag(FieldAccessFlags.Enum, value); }

    bool GetAccessFlag(FieldAccessFlags flag) => Access.HasFlag(flag);
    void SetAccessFlag(FieldAccessFlags flag, bool value)
    {
        if (value)
            Access |= flag;
        else Access &= ~flag;
    }
}