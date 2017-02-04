#include "Phone.h"
#include <iostream>
#define NUMBER_SIZE 12

using namespace std;

const char* strPhoneMarks[3]{
	"samsung",
	"apple",
	"panasonic"
};

Phone::Phone()
{
	initMemory();

	_phoneMark = none;
	_number = "None";
}

Phone::Phone(PhoneMark phoneMark, char * number)
{
	initMemory();

	_phoneMark = phoneMark;

	if (strlen(number) >= NUMBER_SIZE) {
		cout << "number_size_error";
	}
	else {
		strcpy_s(_number, NUMBER_SIZE,number);
	}
}


Phone::~Phone()
{
	if (_number) {
		delete[] _number;
		_number = NULL;
	}
}

void Phone::setOwner(Person * owner)
{
	if (_owner == NULL) {
		_owner = owner;
	}
	else if (_owner->getAge() > owner->getAge()) {
		cout << "Возраст текущего владельца больше возраста нового" << endl;
	}
	else {
		_owner = owner;
	}
}

void Phone::printConsole()
{
	cout << "Информация о телефоне" << endl;
	cout << "Марка: " << strPhoneMarks[_phoneMark] << endl;
	cout << "Номер: " << _number << endl;

	if (_owner) {
		_owner->printToConsole();
	}
	cout << "***********************" << endl;
}

void Phone::initMemory()
{
	_number = new char[12];
	if (!_number) {
		cout << "_number Memory errror";
	}
	*_number = '\0';
}
