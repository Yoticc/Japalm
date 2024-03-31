class class_file
{
    public u4 magic;
    public u2 minor_version;
    public u2 major_version;
    public u2 constant_pool_count;
    public cp_info[] constant_pool;
    public u2 access_flags;
    public u2 this_class;
    public u2 suped_class; 
    public u2 interfaces_count;
    public u2[] interfaces;
    public u2 fields_count;
    public field_info[] fields;
    public u2 methods_count;
    public method_info[] methods;
    public u2 attributes_count;
    public attribute_info[] attributes;
}