using System.Runtime.InteropServices;

namespace ObjectsLayoutDemo;

public class PackingFieldDemo
{
    public static unsafe void Run() {
        object o = (object) 11L;
        //object o = new ManagedO();
        TypedReference tr = __makeref(o);
        IntPtr ptr = **(IntPtr**)(&tr);
        Console.WriteLine(Marshal.ReadInt64(ptr + sizeof(IntPtr)));
    }

    private class ManagedO
    {
        private DateTime _dateTime = DateTime.Now;
        //private long i = 111;
    }
}