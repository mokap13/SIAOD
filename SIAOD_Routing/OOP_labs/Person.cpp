#include "Person.h"
#include <string>
#include <iostream>

#define STRING_SIZE 20

using namespace std;

Person::Person()
{
	initMemory();

	setName("None");
	setFamilia("None");
	setOtchestvo("None");
	setAge(199);
}

Person::Person(char *name, char *familia, char *otchestvo, int age)
{
	initMemory();

	setName(name);
	setFamilia(familia);
	setOtchestvo(otchestvo);
	setAge(age);
}

Person::~Person()
{
	if (_name) {
		delete[] _name;
		_name = NULL;
	}
	if (_familia) {
		delete[] _familia;
		_familia = NULL;
	}
	if (_otchestvo) {
		delete[] _otchestvo;
		_otchestvo = NULL;
	}
	
}

void Person::setName(char *name)
{
	if (strlen(name) >= STRING_SIZE) {
		cout << "name_size_error";
		return;
	}
	strcpy_s(_name,STRING_SIZE, name);
}

void Person::setFamilia(char *familia)
{
	if (strlen(familia) >= STRING_SIZE) {
		cout << "familia_size_error";
		return;
	}
	strcpy_s(_familia, STRING_SIZE, familia);
}

void Person::setOtchestvo(char *otchestvo)
{
	if (strlen(otchestvo) >= STRING_SIZE) {
		cout << "otchestvo_size_error";
		return;
	}
	strcpy_s(_otchestvo, STRING_SIZE, otchestvo);
}

void Person::setAge(int age)
{
	_age = age;
}

char * Person::getName()
{
	return _name;
}

char * Person::getFamilia()
{
	return _familia;
}

char * Person::getOtchestvo()
{
	return _otchestvo;
}

int Person::getAge()
{
	return _age;
}

void Person::printToConsole()
{
	cout << "Информация о человеке" << endl;
	cout << "Фамилия: " << getFamilia() << endl;
	cout << "Имя: " << getName() << endl;
	cout << "Отчество: " << getOtchestvo() << endl;
	cout << "Возраст: " << getAge() << endl;
	cout << "***********************" << endl;
}

void Person::initMemory()
{
	_name = new char[20];
	if (!_name) {
		cout << "_name Memory error\n";
	}
	*_name = '\0';

	_familia = new char[20];
	if (!_familia) {
		cout << "_familia Memory error\n";
	}
	*_familia = '\0';

	_otchestvo = new char[20];
	if (!_otchestvo) {
		cout << "_otchestvo Memory error\n";
	}
	*_otchestvo = '\0';
}
