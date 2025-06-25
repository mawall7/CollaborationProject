* Kompilera först test.cpp till ett dynamic library i visual studio eller g++ (c++ program) eller gcc (c program)
	g++ -o libTest.dll -fPIC -shared test.cpp
	gcc -o libTest.dll -fPIC -shared test.cpp
	(libTest.so heter det i Linux)
* Sen kan du kompiler .cs filen. Tror jag skrivit om allt rätt för dig, bytt från .so till .dll.
	Har kommenterat bort Print delen för du behöver kompilera unsafe för att kunna skicka en sträng till dll:en.
	Prova ta bort och se om du kan skicka strängar, borde fungera.



	-- UPDATE --
	* Kan ge en warning! när du kompilerar c++ koden. Gäller bara läsa en sträng, har inget med stbi_image att göra så bara att kommentera bort.
		Anledningen, klagar på att en char* måste vara en konstant men om jag sätter den till det kan jag ju inte returnera den!
		Ska komma på lösning! Första gången jag stöter på. Brukar inte returnera char* men nu, mellan c# och c++ är nog det bästa sättet.
	* Ingen snygg lösning att man måste spara converted image separat men snabblösning!!!
	* Ska försöka ladda in image till c#. Då skulle man ju i princip kunna visa eller redigera bilden i c#.
