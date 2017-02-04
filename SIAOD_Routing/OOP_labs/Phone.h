#pragma once
#include "Person.h"

extern const char* strPhoneMarks[3];

enum PhoneMark
{
	samsung,
	apple,
	panasonic,
	none
};

class Phone
{
public:
	Phone();
	Phone(PhoneMark phoneMark, char* number);
	~Phone();

	void setOwner(Person *owner);
	void printConsole();
private:
	Person *_owner;
	PhoneMark _phoneMark;
	char* _number;

	void initMemory();
};





