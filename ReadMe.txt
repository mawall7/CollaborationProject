----- NY UPDATE ----- 8 juli 2025
Dom nya (och enda filerna du behöver + stbi_image.h) är
StbImageCSharp.cpp:
	c filen som laddar in stbi_image och som blir libStbImageCSharp.dll(eller so[linux]) och laddas in i ...
StbImage.cs:
	Här laddas dll:en och här ligger Image struct, och viktiga funktioner. Det är här all gentlig kod ligger.
Main_Example_1.cs:
	Bara ett exempel på när man laddar in i C# och visar bild i fönster.

Ett förslag är att vi skapar en ny fil för filter. Super enkelt nu, du slipper pekare ;-).





-- UPDATE --
	* Kan ge en warning! när du kompilerar c++ koden. Gäller bara läsa en sträng, har inget med stbi_image att göra så bara att kommentera bort.
		Anledningen, klagar på att en char* måste vara en konstant men om jag sätter den till det kan jag ju inte returnera den!
		Ska komma på lösning! Första gången jag stöter på. Brukar inte returnera char* men nu, mellan c# och c++ är nog det bästa sättet.
	* Ingen snygg lösning att man måste spara converted image separat men snabblösning!!!
	* Ska försöka ladda in image till c#. Då skulle man ju i princip kunna visa eller redigera bilden i c#.
	* !!! Kolla så att bilden ligger i rätt mapp !!!



* Kompilera först test.cpp till ett dynamic library i visual studio eller g++ (c++ program) eller gcc (c program)
	g++ -o libTest.dll -fPIC -shared test.cpp
	gcc -o libTest.dll -fPIC -shared test.cpp
	(libTest.so heter det i Linux)
* Sen kan du kompiler .cs filen. Tror jag skrivit om allt rätt för dig, bytt från .so till .dll.
	Har kommenterat bort Print delen för du behöver kompilera unsafe för att kunna skicka en sträng till dll:en.
	Prova ta bort och se om du kan skicka strängar, borde fungera.



