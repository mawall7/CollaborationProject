* Kompilera först test.cpp till ett dynamic library i visual studio eller g++ (c++ program) eller gcc (c program)
	g++ -o libTest.dll -fPIC -shared test.cpp
	gcc -o libTest.dll -fPIC -shared test.cpp
	(libTest.so heter det i Linux)
* Sen kan du kompiler .cs filen. Tror jag skrivit om allt rätt för dig, bytt från .so till .dll.
	Har kommenterat bort Print delen för du behöver kompilera unsafe för att kunna skicka en sträng till dll:en.
	Prova ta bort och se om du kan skicka strängar, borde fungera.
	
