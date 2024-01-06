using ObjectsLayoutDemo;
using ObjectLayoutInspector;

TypeLayout.PrintLayout<object>();
TypeLayout.PrintLayout<string>();

PackingFieldDemo.ReadObjectUsingTypedReference();
PackingFieldDemo.ReadObjectUsingNewLowLevelSyntax();
return;

// static unsafe void MakeStringOnStack()
// {
//     string dummy = "qq";
//     var dummyPtr = &dummy;
//     // Initialize the layout
//     var layout = new StringMemoryLayout()
//     {
//         ObjectHeader = Marshal.ReadInt64((IntPtr)dummyPtr),
//         MethodTablePtr = Marshal.ReadInt64((IntPtr)dummyPtr + sizeof(IntPtr)),
//         StringLength = 1,
//         FirstChar = 'a'
//     };
//     Console.WriteLine(dummy);
//
//     var ptr = &layout;
//
//     // Cast the pointer to a string reference
//     string str = *(string*)ptr;
//     string* strPtr = *&str;
//     Console.WriteLine(ptr == strPtr);
//     Console.WriteLine(strPtr == dummyPtr);
//     Console.WriteLine(ptr == dummyPtr);
//     
//     var firstChar = *(char*)(ptr + sizeof(IntPtr) * 2 + sizeof(int));
//     Console.WriteLine(firstChar);
//
//     Console.WriteLine(str);
// }
//
// [StructLayout(LayoutKind.Explicit, Size = 24)]
// public struct StringMemoryLayout
// {
//     [FieldOffset(0)]
//     public long ObjectHeader;
//
//     [FieldOffset(8)]
//     public long MethodTablePtr;
//
//     [FieldOffset(16)]
//     public int StringLength;
//
//     [FieldOffset(20)]
//     public char FirstChar;
//
//     // The padding is implicit here between the FirstChar and the total size of the structure
// }