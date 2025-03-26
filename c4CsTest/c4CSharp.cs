using System.Collections;
using System;
using System.Runtime.InteropServices;

namespace c4CsTest
{
    internal class c4CSharp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C qlStrArg:");
            QlDllC.qlStrArg("string literal from <C#>\n");

            int a = 3; int b = 5;
            int _sum = QlDllC.qlIntRetArgs(a, b);
            Console.WriteLine($"C qlIntRetArgs: {a} + {b} = {_sum}");

            double x = 2.3; double y = 7;
            double _sum_d = QlDllC.qlDblRetArgs(x, y);
            Console.WriteLine($"C qlDblRetArgs: {x} + {y} = {_sum_d}");

            double[] _da8 = new double[8];
            foreach (int i in Enumerable.Range(0, 8).ToArray()) _da8[i] = i * i;
            double _std = QlDllC.qlArrayArg(_da8, _da8.Length);
            Console.WriteLine($"C qlArrayArg(std): {_std}\n");

            // lambda <ArgType, RetType>
            Func<double[], string> arr2Str = da3 => $"[{da3[0]}, {da3[1]}, {da3[2]}]"; 

            double[] _da3 = new double[3];
            QlDllC.qlArrayFetch(_da3, _da3.Length);
            Console.WriteLine($"C qlArrayFetch: {arr2Str(_da3)}");
            //test
            //for (int k = 0; k < 3; ++k) _da3[k] = 80.1 - 20 * k;
            //Console.WriteLine($"C# Test: {arr2Str(_da3)}");

            // copy IntPtr to double[]
            Marshal.Copy(QlDllC.qlArrayRet(3), _da3, 0, 3);
            Console.WriteLine($"C qlArrayRet: [{String.Join(",",_da3)}]\n");

            Cs_struct _xyz = new Cs_struct();
            QlDllC.qlStructFetch(ref _xyz);
            Console.WriteLine($"C qlStructFetch: {_xyz.ToString()}");

            _xyz = QlDllC.qlStructRet();
            Console.WriteLine($"C qlStructRet: {_xyz.ToString()}");
        }
    }
}
