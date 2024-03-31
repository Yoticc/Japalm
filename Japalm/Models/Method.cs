namespace Japalm;
public class Method
{
    public MethodAccessFlags Access;
    public string Name;
    public string Descriptor;
    public List<Attribute> Attributes;

    public bool IsPublic { get => GetAccessFlag(MethodAccessFlags.Public); set => SetAccessFlag(MethodAccessFlags.Public, value); }
    public bool IsPrivate { get => GetAccessFlag(MethodAccessFlags.Private); set => SetAccessFlag(MethodAccessFlags.Private, value); }
    public bool IsProtected { get => GetAccessFlag(MethodAccessFlags.Protected); set => SetAccessFlag(MethodAccessFlags.Protected, value); }
    public bool IsStatic { get => GetAccessFlag(MethodAccessFlags.Static); set => SetAccessFlag(MethodAccessFlags.Static, value); }
    public bool IsFinal { get => GetAccessFlag(MethodAccessFlags.Final); set => SetAccessFlag(MethodAccessFlags.Final, value); }
    public bool IsSynchronized { get => GetAccessFlag(MethodAccessFlags.Synchronized); set => SetAccessFlag(MethodAccessFlags.Synchronized, value); }
    public bool IsBridge { get => GetAccessFlag(MethodAccessFlags.Bridge); set => SetAccessFlag(MethodAccessFlags.Bridge, value); }
    public bool IsVarargs { get => GetAccessFlag(MethodAccessFlags.Varargs); set => SetAccessFlag(MethodAccessFlags.Varargs, value); }
    public bool IsNative { get => GetAccessFlag(MethodAccessFlags.Native); set => SetAccessFlag(MethodAccessFlags.Native, value); }
    public bool IsAbstract { get => GetAccessFlag(MethodAccessFlags.Abstract); set => SetAccessFlag(MethodAccessFlags.Abstract, value); }
    public bool IsStrict { get => GetAccessFlag(MethodAccessFlags.Strict); set => SetAccessFlag(MethodAccessFlags.Strict, value); }
    public bool IsSynthetic { get => GetAccessFlag(MethodAccessFlags.Synthetic); set => SetAccessFlag(MethodAccessFlags.Synthetic, value); }

    bool GetAccessFlag(MethodAccessFlags flag) => Access.HasFlag(flag);
    void SetAccessFlag(MethodAccessFlags flag, bool value)
    {
        if (value)
            Access |= flag;
        else Access &= ~flag;
    }
}