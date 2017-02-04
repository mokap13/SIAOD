#include "PassangerCar.h"
#include <iostream>

PassangerCar::~PassangerCar()
{
	if (_name) {
		delete[] _name;
		_name = NULL;
	}
}
