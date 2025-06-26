using System;
using System.Runtime.InteropServices;

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
	unsafe public static extern void Print([MarshalAs(UnmanagedType.LPStr)]String text); // char* type

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	unsafe public static extern IntPtr GetMessage(); // char* type

// stb_image ////////////////////////////////////////////////////////////
	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void LoadImage([MarshalAs(UnmanagedType.LPStr)]String path, bool flip);

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void LoadImageInfo([MarshalAs(UnmanagedType.LPStr)]String path);

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FreeImage();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetImageWidth();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetImageHeight();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetImageChannels();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetImageSize();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SaveImagePNG([MarshalAs(UnmanagedType.LPStr)]String path);

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SaveImageJPG([MarshalAs(UnmanagedType.LPStr)]String path, int quality);

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SaveImageBMP([MarshalAs(UnmanagedType.LPStr)]String path);

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SaveImageTGA([MarshalAs(UnmanagedType.LPStr)]String path);

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void ConvertToGray();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void ConvertToSepia();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void FreeImageConverted();

	[DllImport("libTest.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SaveImageJPGConverted([MarshalAs(UnmanagedType.LPStr)]String path, int quality);
/////////////////////////////////////////////////////////////////////////
    //  dummy comment
	public static void Main () {
/*		
		int first = 10, second = 5;
		Console.WriteLine("Input are!");
		Console.WriteLine("First: " + first);
		Console.WriteLine("Second: " + second);

		Console.WriteLine("Add: " + Add(first, second));
		Console.WriteLine("Subtract: " + Subtract(first, second));
		Console.WriteLine("Multiply: " + Multiply(first, second));
		PrintOS();
*/
		unsafe {
			//char[] text = "Hello there!";
			Print("Hello there!");

            IntPtr MsgP = GetMessage();
            string MsgStr = Marshal.PtrToStringAnsi(MsgP);
            Console.WriteLine("Message = \"{0}\"", MsgStr);
        }

        // stb_image ////////////////////////////////////////////////////////////
        LoadImage("sky.png", false); // LoadImage(path, flip)
		int width = GetImageWidth();
		int height = GetImageHeight();
		int channels = GetImageChannels();
		int size = GetImageSize();
		//                                                                                1 = Gray 3 = RGB 4 = RGBA (A = alpha = transparent)
		Console.WriteLine("Image Info: {0}x{1} : channels {2} : size {3}", width, height, channels, size); // size error!!!???
		//LoadImageInfo("sky.png"); // Only loads info (for displaying info), not image!
		//FreeImage();
		//SaveImagePNG("sky2.png");
		SaveImageJPG("sky.jpg", 80); // SaveImageJPG(path, quality)    80 = average
		//SaveImageBMP("sky.bmp");
		//SaveImageTGA("sky.tga");

		ConvertToGray();
		SaveImageJPGConverted("sky_gray.jpg", 80);
		FreeImageConverted();
		ConvertToSepia();
		SaveImageJPGConverted("sky_sepia.jpg", 80);
////////////////////////////////////////////////////////////////////////

	}
}
