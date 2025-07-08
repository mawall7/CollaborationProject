using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace MWFTestApplication {

	class MainWindow : System.Windows.Forms.Form {	

		private static PictureBox pictureBox1;
		private static StbImage.Image Image1;

		public static void Main (string[] args) {

			/////////////////////////////////////////////////////////////////////////
			Image1 = new StbImage.Image();

			// Manuelt
			/*
			IntPtr DataPtr = StbImage.Load("sky.png", ref Image1.Width, ref Image1.Height, ref Image1.Channels);
			Image1.Size = Image1.Width * Image1.Height * Image1.Channels;
			Console.WriteLine("{0} {1} {2} {3}", Image1.Width, Image1.Height, Image1.Channels, Image1.Size);
			Image1.Data = new byte[Image1.Size];
			Marshal.Copy(DataPtr, Image1.Data, 0, Image1.Size);
			*/
			// Eller färdig funktion
			Image1.LoadFromFile("sky.png");

			// Andra Funktioner
			//StbImage.Set_Flip_Verticaly_On_Load(false);
				// Måste stå innan man laddar
			//StbImage.Info("sky.png", ref Width, ref Height, ref Channels);
				// Ska skriva funktion till så det ser likadant ut som när man laddar bild.
			/////////////////////////////////////////////////////////////////////////

			try {
				Application.Run(new MainWindow());

			} catch (Exception e) {
				Console.ForegroundColor = ConsoleColor.Red; 
				Console.WriteLine("Error: " + e.StackTrace);
			}

		}

		public MainWindow () {

			ClientSize = new System.Drawing.Size (500, 325);
			Text = "Stbi Image Example 1";

			pictureBox1 = new PictureBox();
			pictureBox1.Location = new Point(10, 10);
			//pictureBox1.Width = 400; pictureBox1.Height = 400;
			pictureBox1.Size = new Size(400, 400);
			pictureBox1.BackColor = Color.Black;
			var rgbData = Image1.Convert8BitRgbaToRgb24();
			Bitmap bmp = StbImage.CreateBitmapFromBytes(rgbData, Image1.Width, Image1.Height);
			pictureBox1.Image = bmp.GetThumbnailImage(400, 400, null, IntPtr.Zero);
			this.Controls.Add(pictureBox1);

		}

	}
}
