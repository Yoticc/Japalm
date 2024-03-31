namespace Japalm;
public class Class
{
    public u4 Magic;
    public ClassVersion Version;
    public List<ConstantPoolEntry?> ConstantPool;
    public ClassAccessFlags Access;
    public ClassCP This;
    public ClassCP? Super;
    public List<ClassCP> Interfaces;
    public List<Field> Fields;
    public List<Method> Methods;
    public List<Attribute> Attributes;

    public bool IsPublic { get => GetAccessFlag(ClassAccessFlags.Public); set => SetAccessFlag(ClassAccessFlags.Public, value); }
    public bool IsFinal { get => GetAccessFlag(ClassAccessFlags.Final); set => SetAccessFlag(ClassAccessFlags.Final, value); }
    public bool IsSuper { get => GetAccessFlag(ClassAccessFlags.Super); set => SetAccessFlag(ClassAccessFlags.Super, value); }
    public bool IsInterface { get => GetAccessFlag(ClassAccessFlags.Interface); set => SetAccessFlag(ClassAccessFlags.Interface, value); }
    public bool IsAbstract { get => GetAccessFlag(ClassAccessFlags.Abstract); set => SetAccessFlag(ClassAccessFlags.Abstract, value); }
    public bool IsSynthetic { get => GetAccessFlag(ClassAccessFlags.Synthetic); set => SetAccessFlag(ClassAccessFlags.Synthetic, value); }
    public bool IsAnnotation { get => GetAccessFlag(ClassAccessFlags.Annotation); set => SetAccessFlag(ClassAccessFlags.Annotation, value); }
    public bool IsEnum { get => GetAccessFlag(ClassAccessFlags.Enum); set => SetAccessFlag(ClassAccessFlags.Enum, value); }

    bool GetAccessFlag(ClassAccessFlags flag) => Access.HasFlag(flag);
    void SetAccessFlag(ClassAccessFlags flag, bool value)
    {
        if (value)
            Access |= flag;
        else Access &= ~flag;
    }
}