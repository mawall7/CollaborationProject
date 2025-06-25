using System;
using System.Runtime.InteropServices;
//ToDo 1 : Make test run

class Program {
    [DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Add(int a, int b);

    [DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Subtract(int a, int b);

    [DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Multiply(int a, int b);

    [DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void PrintOS();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Print([MarshalAs(UnmanagedType.LPStr)]String text); // char* type

	public static void Main () {

		int first = 10, second = 5;
		Console.WriteLine("Input are !");
		Console.WriteLine("First : " + first);
		Console.WriteLine("second : " + second);

		Console.WriteLine("Add : " + Add(first, second));
		Console.WriteLine("Subtract : " + Subtract(first, second));
		Console.WriteLine("Multiply : " + Multiply(first, second));
		PrintOS();
        //unsafe
        //{
            //char[] text = "Hello there!";
            Print("Hello there!");
        //}
    }
}
