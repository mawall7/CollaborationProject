
#include <iostream>
#include <string>
#include "imgConv.cpp"

imgcon::Image img, img_bak;
struct File {
	std::string name;
	std::string format;
	std::string path;
};
File g_file;

void println (char* text) {
	std::cout << text << '\n'; 
}

const int checkOS () {
	#if defined(__linux__) // Or #if __linux__
		return 1;
	#elif _WIN32
		#if defined(_WIN64)
			return 2;
		#else
			return 3;
		#endif
	#elif __FreeBSD__
			return 4;
	#elif __ANDROID__
			return 5;
	#elif __APPLE__
			return 6;
	#else
			return 7;
	#endif
}

void printOS () {
	int os = checkOS();
	switch (os) {
		case 1:
			std::cout << "OS: GNU/Linux!" << '\n';
			break;
		case 2:
			std::cout << "OS: Windows 64-bit!" << '\n';
			break;
		case 3:
			std::cout << "OS: Windows!" << '\n';
			break;
		case 4:
			std::cout << "OS: FreeBSD!" << '\n';
			break;
		case 5:
			std::cout << "OS: Android!" << '\n';
			break;
		case 6:
			std::cout << "OS: macOS!" << '\n';
			break;
		case 7:
			std::cout << "OS: Other!" << '\n';
			break;
	}
}


extern "C" {

	void Print (char* text) {
		println(text);
	}

	char* GetMessage () {
		char* msg = "Hello c# from c++!";
		return msg;
	}

	void PrintOS () {
		printOS();
	}

	int Add (int a, int b) {
		return a + b;
	}

	int Subtract (int a, int b) {
		return a - b;
	}

	int Multiply (int a, int b) {
		return a * b;
	}

// stb_image ////////////////////////////////////////////////////////////
	void LoadImage (char* path, bool flip) {
		imgcon::image_load (img, path, flip);
	}

	void LoadImageInfo (char* path) {
		image_info (img, path);
	}

	void FreeImage () {
		imgcon::image_free(img);
	}

	int GetImageWidth () { return img.width; }

	int GetImageHeight () { return img.height; }

	int GetImageChannels () { return img.channels; }

	int GetImageSize () { return img.size; }

	void SaveImagePNG (char* path) {
		imgcon::image_save_png (img, path);
	}

	void SaveImageJPG (char* path, int quality) {
		imgcon::image_save_jpg (img, path, quality);
	}

	void SaveImageBMP (char* path) {
		imgcon::image_save_bmp (img, path);
	}

	void SaveImageTGA (char* path) {
		imgcon::image_save_tga (img, path);
	}


	void ConvertToGray () {
		imgcon::image_to_gray (img, img_bak);
	}

	void ConvertToSepia () {
		imgcon::image_to_sepia (img, img_bak);
	}

	void FreeImageConverted () {
		imgcon::image_free(img_bak);
	}

	void SaveImageJPGConverted (char* path, int quality) {
		imgcon::image_save_jpg (img_bak, path, quality);
	}
////////////////////////////////////////////////////////////////////////

}

