using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

class StbImage {

	[DllImport("libStbImageCSharp.dll", CallingConvention = CallingConvention.Cdecl)] // , CharSet = CharSet.Ansi
	public static extern IntPtr Load ([MarshalAs(UnmanagedType.LPStr)]String Path, ref int Width, ref int Height, ref int Channels);

	[DllImport("libStbImageCSharp.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Set_Flip_Verticaly_On_Load (bool Value);

	[DllImport("libStbImageCSharp.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Info ([MarshalAs(UnmanagedType.LPStr)]String Path, ref int Width, ref int Height, ref int Channels);

//	[DllImport("libStbImageCSharp.so", CallingConvention = CallingConvention.Cdecl)]
//	public static extern void Image_Free ([MarshalAs(UnmanagedType.LPArray)] byte[] Data);

	public struct Image {
		public int Width;
		public int Height;
		public int Channels;
		public int Size;
		public byte[] Data;

/*		public StbiImage (int Value) {
			Width = Value;
			Height = Value;
			Channels = Value;
		}*/

		public void LoadFromFile(string path) {
			IntPtr DataPtr = StbImage.Load(path, ref Width, ref Height, ref Channels);
			Size = Width * Height * Channels;
			//Console.WriteLine("{0} {1} {2} {3}", Width, Height, Channels, Size);
			Data = new byte[Size];
			Marshal.Copy(DataPtr, Data, 0, Size);
		}

/*		private static byte[] Convert16BitGrayScaleToRgb48 (byte[] inBuffer, int width, int height) {
			int inBytesPerPixel = 2;
			int outBytesPerPixel = 6;
			byte[] outBuffer = new byte[width * height * outBytesPerPixel];
			int inStride = width * inBytesPerPixel;
			int outStride = width * outBytesPerPixel; // Step through the image by row 
			for (int y = 0; y < height; y++) { // Step through the image by column
				for (int x = 0; x < width; x++) { // Get inbuffer index and outbuffer index
					int inIndex = (y * inStride) + (x * inBytesPerPixel);
					int outIndex = (y * outStride) + (x * outBytesPerPixel);

					byte hibyte = inBuffer[inIndex + 1];
					byte lobyte = inBuffer[inIndex];

					//R
					outBuffer[outIndex] = lobyte;
					outBuffer[outIndex + 1] = hibyte;
					//G
					outBuffer[outIndex + 2] = lobyte;
					outBuffer[outIndex + 3] = hibyte;
					//B
					outBuffer[outIndex + 4] = lobyte;
					outBuffer[outIndex + 5] = hibyte;
				}
			}
			return outBuffer;
		}*/

		//private static byte[] Convert8BitRgbaToRgb48 (byte[] inBuffer, int width, int height) {
		public byte[] Convert8BitRgbaToRgb24 () {
			int inBytesPerPixel = 4;
			//int outBytesPerPixel = 6; // ToRgb48
			int outBytesPerPixel = 3;
			byte[] outBuffer = new byte[Width * Height * outBytesPerPixel];
			int inStride = Width * inBytesPerPixel;
			int outStride = Width * outBytesPerPixel; // Step through the image by row 
			for (int y = 0; y < Height; y++) { // Step through the image by column
				for (int x = 0; x < Width; x++) { // Get inbuffer index and outbuffer index
					int inIndex = (y * inStride) + (x * inBytesPerPixel);
					int outIndex = (y * outStride) + (x * outBytesPerPixel);

					outBuffer[outIndex] = Data[inIndex + 2];
					outBuffer[outIndex + 1] = Data[inIndex + 1];
					outBuffer[outIndex + 2] = Data[inIndex];

					// ToRgb48
					/*
					//R
					outBuffer[outIndex] = bbyte;
					outBuffer[outIndex + 1] = rbyte;
					//G
					outBuffer[outIndex + 2] = gbyte;
					outBuffer[outIndex + 3] = gbyte;
					//B
					outBuffer[outIndex + 4] = bbyte;
					outBuffer[outIndex + 5] = bbyte;
					*/

				}
			}
			return outBuffer;
		}

	}

	public static Bitmap CreateBitmapFromBytes (byte[] pixelValues, int width, int height) {
		// Create Image
		//Bitmap bmp = new Bitmap(width, height, PixelFormat.Format48bppRgb);
		Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

		// Get a reference to the images pixel data
		Rectangle dimension = new Rectangle(0, 0, bmp.Width, bmp.Height);
		BitmapData picData = bmp.LockBits(dimension, ImageLockMode.ReadWrite, bmp.PixelFormat);
		IntPtr pixelStartAddress = picData.Scan0;

		// Copy pixel data into the bitmap structure
		System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, pixelStartAddress, pixelValues.Length);

		bmp.UnlockBits(picData);
		return bmp;
	}

}
