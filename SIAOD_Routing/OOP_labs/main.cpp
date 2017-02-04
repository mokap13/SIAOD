#include "Person.h"
#include "Phone.h"
#include <clocale>
#include <cstdlib>

void main() {
	setlocale(LC_ALL, "rus");

	Person *marketManager = new Person("Ivan", "Zdanov", "Sergeevich", 26);
	Person *trader = new Person("Vladimir", "Ivankov", "Dedov", 22);
	
	marketManager->printToConsole();
	trader->printToConsole();

	Phone *phone = new Phone(samsung, "79652346523");
	phone->setOwner(marketManager);
	phone->printConsole();

	delete marketManager;
	delete trader;
	delete phone;

	system("pause");
}