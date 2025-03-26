
using System;
using System.Runtime.InteropServices;

public struct Cs_struct
{
    public Cs_struct(double x, double y, double z)
    {
        X = x; Y = y; Z = z;
    }

    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public override string ToString() => $"[{X}, {Y}, {Z}]";
}

public class QlDllC
{
    //[DllImport("user32.dll", CharSet = CharSet.Auto)]
    //public static extern IntPtr MessageBox(int hWnd, String text,
    //                String caption, uint type);

    // UPDATE your dll path
    const string _C_DLL_PATH = "C:\\qlDev\\libc\\winCDynamic.dll";

    // do NOT use [Auto]; MUST be [Ansi]
    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl, 
        CharSet = CharSet.Ansi)]
    public static extern void qlStrArg(string text);
    // C: void qlStrArg(char *s)

    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
    public static extern int qlIntRetArgs(int x, int y);

    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
    public static extern double qlDblRetArgs(double x, double y);

    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
    public static extern double qlArrayArg(double[] x, int sz);
    // C: double qlArrayArg(double arr[], int size)


    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
    public static extern void qlArrayFetch(double[] x, int sz);

    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr qlArrayRet(int sz);
    // C: void* qlArrayRet()


    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
    public static extern void qlStructFetch(ref Cs_struct ptr);
    //C: void qlStructFetch(C_struct* ptr)

    [DllImport(_C_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
    public static extern Cs_struct qlStructRet();
    // C: struct C_struct {}; C_struct qlStructRet()
}
