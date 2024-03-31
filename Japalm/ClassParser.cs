using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;

namespace Japalm;
public unsafe class ClassParser
{
    static readonly Dictionary<ConstantType, int> ConstantInstanceSizes = new()
    {
        { ConstantType.Integer, 4 },
        { ConstantType.Float, 4 },
        { ConstantType.Long, 8 },
        { ConstantType.Double, 8 },
        { ConstantType.Class, 2 },
        { ConstantType.String, 2 },
        { ConstantType.FieldRef, 4 },
        { ConstantType.MethodRef, 4 },
        { ConstantType.InterfaceMethodRef, 4 },
        { ConstantType.NameAndType, 4 },
        { ConstantType.MethodHandle, 3 },
        { ConstantType.MethodType, 2 },
        { ConstantType.InvokeDynamic, 4 }
    };

    public static Class Parse(byte[] bytes)
    {
        var raw = ParseRaw(bytes);

        var cls = new Class
        {
            Magic = raw.magic,
            Version = new(raw.minor_version, raw.major_version),
            Access = (ClassAccessFlags)raw.access_flags,
            ConstantPool = [],
            Interfaces = [],
            Fields = [],
            Methods = [],
            Attributes = []
        };

        for (var i = 0; i < raw.constant_pool.Length; i++)
        {
            var cp_info = raw.constant_pool[i];
            if (cp_info is null)
            {
                cls.ConstantPool.Add(null);
                continue;
            }

            var (tag, info) = ((ConstantType)cp_info.tag, cp_info.info);

            cls.ConstantPool.Add(tag switch
            {
                ConstantType.Utf8 => new Utf8CP().ParseRaw(raw.constant_pool, info),
                ConstantType.Integer => new IntegerCP().ParseRaw(raw.constant_pool, info),
                ConstantType.Float => new FloatCP().ParseRaw(raw.constant_pool, info),
                ConstantType.Long => new LongCP().ParseRaw(raw.constant_pool, info),
                ConstantType.Double => new DoubleCP().ParseRaw(raw.constant_pool, info),
                ConstantType.Class => new ClassCP().ParseRaw(raw.constant_pool, info),
                ConstantType.String => new StringCP().ParseRaw(raw.constant_pool, info),
                ConstantType.FieldRef => new FieldRefCP().ParseRaw(raw.constant_pool, info),
                ConstantType.MethodRef => new MethodRefCP().ParseRaw(raw.constant_pool, info),
                ConstantType.InterfaceMethodRef => new InterfaceMethodRefCP().ParseRaw(raw.constant_pool, info),
                ConstantType.NameAndType => new NameAndTypeCP().ParseRaw(raw.constant_pool, info),
                ConstantType.MethodHandle => new MethodHandleCP().ParseRaw(raw.constant_pool, info),
                ConstantType.MethodType => new MethodTypeCP().ParseRaw(raw.constant_pool, info),
                ConstantType.InvokeDynamic => new InvokeDynamicCP().ParseRaw(raw.constant_pool, info),
            });
        }

        cls.This = (cls.ConstantPool[raw.this_class] as ClassCP)!;
        cls.Super = (cls.ConstantPool[raw.suped_class] as ClassCP)!;

        foreach (var rawInterface in raw.interfaces)
            cls.Interfaces.Add((cls.ConstantPool[rawInterface] as ClassCP)!);

        foreach (var rawField in raw.fields)
        {
            var field = new Field
            {
                Access = (FieldAccessFlags)rawField.access_flags,
                Name = (cls.ConstantPool[rawField.name_index] as Utf8CP)!.Value,
                Descriptor = (cls.ConstantPool[rawField.descriptor_index] as Utf8CP)!.Value,
                Attributes = []
            };

            foreach (var rawAttribute in rawField.attributes)
            {
                var attribute = new Attribute
                { 
                    Name = (cls.ConstantPool[rawAttribute.attribute_name_index] as Utf8CP)!.Value,
                    Bytes = rawAttribute.info
                };

                field.Attributes.Add(attribute);
            }

            cls.Fields.Add(field);
        }

        foreach (var rawMethod in raw.methods)
        {
            var method = new Method
            {
                Access = (MethodAccessFlags)rawMethod.access_flags,
                Name = (cls.ConstantPool[rawMethod.name_index] as Utf8CP)!.Value,
                Descriptor = (cls.ConstantPool[rawMethod.descriptor_index] as Utf8CP)!.Value,
                Attributes = []
            };

            foreach (var rawAttribute in rawMethod.attributes)
            {
                var attribute = new Attribute
                {
                    Name = (cls.ConstantPool[rawAttribute.attribute_name_index] as Utf8CP)!.Value,
                    Bytes = rawAttribute.info
                };

                method.Attributes.Add(attribute);
            }

            cls.Methods.Add(method);
        }

        foreach (var rawAttribute in raw.attributes)
        {
            var attribute = new Attribute 
            {
                Name = (cls.ConstantPool[rawAttribute.attribute_name_index] as Utf8CP)!.Value,
                Bytes = rawAttribute.info
            };

            cls.Attributes.Add(attribute);
        }

        return cls;
    }

    internal static class_file ParseRaw(byte[] bytes)
    {
        byte* original;
        byte* p;
        fixed (byte* ptr = bytes)
            p = original = ptr;

        var cls = new class_file();

        cls.magic = U4();
        cls.minor_version = U2();
        cls.major_version = U2();
        cls.constant_pool_count = U2();

        cls.constant_pool = new cp_info[cls.constant_pool_count];
        for (var i = 1; i < cls.constant_pool.Length; i++)
        {
            var uncastedType = U1();

            var type = (ConstantType)uncastedType;

            if (type == ConstantType.Utf8)
            {
                var byteCount = U2();

                cls.constant_pool[i] = new()
                {
                    tag = uncastedType,
                    info = MemEx.Read(p - 2, byteCount + 2),
                };

                p += byteCount;
            }
            else
            {
                var size = ConstantInstanceSizes[type];
                cls.constant_pool[i] = new()
                {
                    tag = uncastedType,
                    info = MemEx.Read(p, size)
                };

                p += size;

                if (type == ConstantType.Long || type == ConstantType.Double)
                    i++;
            }
        }

        cls.access_flags = U2();
        cls.this_class = U2();
        cls.suped_class = U2();

        cls.interfaces_count = U2();

        cls.interfaces = new u2[cls.interfaces_count];
        for (var i = 0; i < cls.interfaces_count; i++)
            cls.interfaces[i] = U2();

        cls.fields_count = U2();

        cls.fields = new field_info[cls.fields_count];
        for (var i = 0; i < cls.fields.Length; i++)
        {
            var field = new field_info();

            field.access_flags = U2();
            field.name_index = U2();
            field.descriptor_index = U2();
            field.attributes_count = U2();

            field.attributes = new attribute_info[field.attributes_count];
            for (var o = 0; o < field.attributes.Length; o++)
            {
                var attribute = new attribute_info();

                attribute.attribute_name_index = U2();
                attribute.attribute_length = U4();

                attribute.info = MemEx.Read(p, (int)attribute.attribute_length);
                p += attribute.attribute_length;

                field.attributes[o] = attribute;
            }

            cls.fields[i] = field;
        }

        cls.methods_count = U2();
        cls.methods = new method_info[cls.methods_count];
        for (var i = 0; i < cls.methods_count; i++)
        {
            var method = new method_info();

            method.access_flags = U2();
            method.name_index = U2();
            method.descriptor_index = U2();
            method.attributes_count = U2();

            method.attributes = new attribute_info[method.attributes_count];
            for (var o = 0; o < method.attributes.Length; o++)
            {
                var attribute = new attribute_info();

                attribute.attribute_name_index = U2();
                attribute.attribute_length = U4();

                attribute.info = MemEx.Read(p, (int)attribute.attribute_length);
                p += attribute.attribute_length;

                method.attributes[o] = attribute;
            }

            cls.methods[i] = method;
        }

        cls.attributes_count = U2();
        cls.attributes = new attribute_info[cls.attributes_count];
        for (var i = 0; i < cls.attributes_count; i++)
        {
            var attribute = new attribute_info();

            attribute.attribute_name_index = U2();
            attribute.attribute_length = U4();

            attribute.info = MemEx.Read(p, (int)attribute.attribute_length);
            p += attribute.attribute_length;

            cls.attributes[i] = attribute;
        }

        return cls;

        u1 U1()
        {
            var value = ReadU1(p);
            p += 1;
            return value;
        }

        u2 U2()
        {
            var value = ReadU2(p);
            p += 2;
            return value;
        }

        u4 U4()
        {
            var value = ReadU4(p);
            p += 4;
            return value;
        }
    }
}