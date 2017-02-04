#include "Car.h"
#include <iostream>

#define NAME_SIZE 15

using namespace std;

Car::Car(char* name, int weight)
{
	_name = new char[NAME_SIZE];
	if (!_name) {
		cout << "_name Memory error\n";
	}
	*_name = '\0';

	if (strlen(name) >= NAME_SIZE) {
		cout << "_name_size_error\n";
		return;
	}
	
	strcpy_s(_name, NAME_SIZE, name);

	_weight = weight;
}

Car::~Car()
{
	if (_name) {
		delete[] _name;
		_name = NULL;
	}
}

int Car::getWeight()
{
	return _weight;
}

char * Car::getName()
{
	return _name;
}
