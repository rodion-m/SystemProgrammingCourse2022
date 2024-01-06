using System.Runtime.InteropServices;
using ObjectLayoutInspector;

namespace ObjectsLayoutDemo;

public unsafe class PackingFieldDemo
{
    static readonly int MethodTablePtrSize = sizeof(IntPtr);


    public static void ReadObjectUsingTypedReference()
    {
        object o = (object)11L;
        // Create a TypedReference for the object.
        // This allows for type-safe operations on the object.
        TypedReference tr = __makeref(o);

        // Convert TypedReference to an IntPtr (typed reference pointer).
        // The TypedReference is a special type in C# that can be converted to an IntPtr.
        IntPtr typedRefPointer = *(IntPtr*)(&tr);

        // Dereference the typed reference pointer to get the actual object pointer.
        // This is the memory address of the actual object 'o'.
        IntPtr objPointer = *(IntPtr*)typedRefPointer;

        // Read the value directly from the memory location.
        // Since TypedReference contains a reference to the object,
        // we use Marshal to read the value from the memory location it points to.
        // The offset 'sizeof(IntPtr)' skips the object's type information to directly access the value.
        long value = Marshal.ReadInt64(objPointer + sizeof(IntPtr));

        Console.WriteLine($"{nameof(ReadObjectUsingTypedReference)}: {value}");
    }
    
    public static void ReadObjectUsingNewLowLevelSyntax()
    {
        object o = (object)11L;
        var ptr = &o;
        var value = *(long*)(*(IntPtr*)ptr + MethodTablePtrSize);
        Console.WriteLine($"{nameof(ReadObjectUsingNewLowLevelSyntax)}: {value}");
    }
}