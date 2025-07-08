#pragma once
#ifndef IMAGE_CONVERTER_HEADER
#define IMAGE_CONVERTER_HEADER 1

#define STB_IMAGE_IMPLEMENTATION
#include "libraries/stb_image.h"
#define STB_IMAGE_WRITE_IMPLEMENTATION
#include "libraries/stb_image_write.h"

namespace imgcon {

	enum allocation_type {
		NO_ALLOCATION, SELF_ALLOCATED, STB_ALLOCATED
	};

	struct Image {
		int width;
		int height;
		int channels;
		size_t size;
		uint8_t *data = nullptr;
		enum allocation_type allocation_;

/*		Image (const Image& other) noexcept {
			width = other.width;
			height = other.height;
			channels = other.channels;
			size = other.size;
			memcpy(data, other.data, width * height * channels);
		}*/

		// copy operator constructor
/*		Image& operator=(const Image& other) noexcept {
			//delete[] data;
			width = other.width;
			height = other.height;
			channels = other.channels;
			size = other.size;
			memcpy(data, other.data);

			return *this;
		}*/

		~Image () {
			if (data != nullptr) {
				stbi_image_free(data);
				data = nullptr;
				//std::cout << "free\n";
			}
		}

		Image& operator=(Image&& other) noexcept {
			if (this != &other) {
				//delete[] data;
				width = other.width;
				height = other.height;
				channels = other.channels;
				size = other.size;
				data = other.data;

				other.data = nullptr;
			}
			return *this;
		}
	};

	// stbi_uc* pixels = stbi_load("textures/texture.jpg", &texWidth, &texHeight, &texChannels, STBI_rgb_alpha);
	void image_load (Image& img, const char *fname, bool flip = false) {
		if (flip) stbi_set_flip_vertically_on_load(true);
		img.data = stbi_load(fname, &img.width, &img.height, &img.channels, 0);
		if (img.data == NULL) {
			std::cout << "Error in loading the image\n";
			exit(1);
		}
		img.size = img.width * img.height * img.channels;
		if (flip) stbi_set_flip_vertically_on_load(false);
	}

	void image_info (Image& img, const char *fname) {
		stbi_info(fname, &img.width, &img.height, &img.channels);
		img.size = img.width * img.height * img.channels;
	}

	void image_to_gray (const Image& img, Image& img_gray) {
		img_gray.width = img.width;
		img_gray.height = img.height;
		img_gray.channels = img.channels == 4 ? 2 : 1;
		img_gray.size = img_gray.width * img_gray.height * img_gray.channels;

		img_gray.data = (uint8_t*)malloc(img_gray.size);
		if (img_gray.data == NULL) {
			printf("Unable to allocate memory for the gray image.\n");
			exit(1);
		}

		for (unsigned char *p = img.data, *pg = img_gray.data; p != img.data + img.size; p += img.channels, pg += img_gray.channels) {
			*pg = (uint8_t)((*p + *(p + 1) + *(p + 2))/3.0);
			//*pg = (uint8_t)((std::max(*p, std::max(*(p + 1), *(p + 2)))));
			if (img.channels == 4) {
				*(pg + 1) = *(p + 3);
			}
		}
	}

	void image_to_sepia (const Image& img, Image& img_sepia) {
		img_sepia.width = img.width;
		img_sepia.height = img.height;
		img_sepia.channels = img.channels;
		img_sepia.size = img.width * img.height * img.channels;

		img_sepia.data = (uint8_t*)malloc(img.size);
		if (img_sepia.data == NULL) {
			printf("Unable to allocate memory for the sepia image.\n");
			exit(1);
		}

		// Sepia filter coefficients from https://stackoverflow.com/questions/1061093/how-is-a-sepia-tone-created
		for (unsigned char *p = img.data, *pg = img_sepia.data; p != img.data + img.size; p += img.channels, pg += img.channels) {
			*pg       = (uint8_t)fmin(0.393 * *p + 0.769 * *(p + 1) + 0.189 * *(p + 2), 255.0);         // red
			*(pg + 1) = (uint8_t)fmin(0.349 * *p + 0.686 * *(p + 1) + 0.168 * *(p + 2), 255.0);         // green
			*(pg + 2) = (uint8_t)fmin(0.272 * *p + 0.534 * *(p + 1) + 0.131 * *(p + 2), 255.0);         // blue        
			if (img.channels == 4) {
				*(pg + 3) = *(p + 3);
			}
		}
	}

/*
	void image_to_sepia2 (const Image& img, Image& img_sepia) {
		img_sepia.width = img.width;
		img_sepia.height = img.height;
		img_sepia.channels = img.channels;
		img_sepia.size = img.width * img.height * img.channels;

		img_sepia.data = (uint8_t*)malloc(img.size);
		if (img_sepia.data == NULL) {
			printf("Unable to allocate memory for the sepia image.\n");
			exit(1);
		}

		// Sepia filter coefficients from https://stackoverflow.com/questions/1061093/how-is-a-sepia-tone-created
		for (unsigned char *p = img.data, *pg = img_sepia.data; p != img.data + img.size; p += img.channels, pg += img.channels) {
			*pg       = (uint8_t)fmin(0.272 * *p + 0.534 * *(p + 1) + 0.131 * *(p + 2), 255.0);         // red
			*(pg + 1) = (uint8_t)fmin(0.393 * *p + 0.769 * *(p + 1) + 0.189 * *(p + 2), 255.0);         // green
			*(pg + 2) = (uint8_t)fmin(0.349 * *p + 0.686 * *(p + 1) + 0.168 * *(p + 2), 255.0);         // blue        
			if (img.channels == 4) {
				*(pg + 3) = *(p + 3);
			}
		}
	}

	void image_to_sepia3 (const Image& img, Image& img_sepia) {
		img_sepia.width = img.width;
		img_sepia.height = img.height;
		img_sepia.channels = img.channels;
		img_sepia.size = img.width * img.height * img.channels;

		img_sepia.data = (uint8_t*)malloc(img.size);
		if (img_sepia.data == NULL) {
			printf("Unable to allocate memory for the sepia image.\n");
			exit(1);
		}

		// Sepia filter coefficients from https://stackoverflow.com/questions/1061093/how-is-a-sepia-tone-created
		for (unsigned char *p = img.data, *pg = img_sepia.data; p != img.data + img.size; p += img.channels, pg += img.channels) {
			*pg       = (uint8_t)fmin(0.349 * *p + 0.686 * *(p + 1) + 0.168 * *(p + 2), 255.0);         // red
			*(pg + 1) = (uint8_t)fmin(0.272 * *p + 0.534 * *(p + 1) + 0.131 * *(p + 2), 255.0);         // green
			*(pg + 2) = (uint8_t)fmin(0.393 * *p + 0.769 * *(p + 1) + 0.189 * *(p + 2), 255.0);         // blue        
			if (img.channels == 4) {
				*(pg + 3) = *(p + 3);
			}
		}
	}
*/

	void image_save_png (const Image& img, const char *fname) {
		stbi_write_png(fname, img.width, img.height, img.channels, img.data, img.width * img.channels);
	}

	void image_save_jpg (const Image& img, const char *fname, int quality) {
		quality = std::min(quality, 100);
		quality = std::max(quality, 0);
		stbi_write_jpg(fname, img.width, img.height, img.channels, img.data, quality);
	}

	void image_save_bmp (const Image& img, const char *fname) {
		stbi_write_bmp(fname, img.width, img.height, img.channels, img.data);
	}

	void image_save_tga (const Image& img, const char *fname) {
		stbi_write_tga(fname, img.width, img.height, img.channels, img.data);
	}

	void image_free (Image& img) {
		stbi_image_free(img.data);
	}


/*	void image_load(Image* img, const char *fname) {
		img->data = stbi_load(fname, &img->width, &img->height, &img->channels, 0);
		if(img->data == NULL) {
			std::cout << "Error in loading the image\n";
			exit(1);
		}
	}*/

}

/*
void Image_create(Image *img, int width, int height, int channels, bool zeroed);
*/


#endif
