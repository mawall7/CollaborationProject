
#include <iostream>

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

}

