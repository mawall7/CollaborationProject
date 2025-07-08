
#define STB_IMAGE_IMPLEMENTATION
#include "libraries/stb_image.h"
#define STB_IMAGE_WRITE_IMPLEMENTATION
#include "libraries/stb_image_write.h"

extern "C" {

	uint8_t* Load (char* Path, int& Width, int& Height, int& Channels) {
		return stbi_load(Path, &Width, &Height, &Channels, 0); // 0 - (no change) desired channels
	}

	void Set_Flip_Verticaly_On_Load (bool Value) {
		stbi_set_flip_vertically_on_load(Value);
	}

	void Info (char* Path, int& Width, int& Height, int& Channels) {
		stbi_info(Path, &Width, &Height, &Channels);
	}

/*	void Image_Free (uint8_t* Data) {
		stbi_image_free(Data);
	}*/

	void Write_PNG (char* Path, int Width, int Height, int Channels, uint8_t* Data) {
		stbi_write_png(Path, Width, Height, Channels, Data, Width * Channels);
	}

	void Write_JPG (char* Path, int Width, int Height, int Channels, uint8_t* Data, int Quality) {
		stbi_write_jpg(Path, Width, Height, Channels, Data, Quality);
	}

	void Write_BMP (char* Path, int Width, int Height, int Channels, uint8_t* Data) {
		stbi_write_bmp(Path, Width, Height, Channels, Data);
	}

	void Write_TGA (char* Path, int Width, int Height, int Channels, uint8_t* Data) {
		stbi_write_tga(Path, Width, Height, Channels, Data);
	}
}
