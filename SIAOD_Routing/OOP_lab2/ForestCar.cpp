#include "ForestCar.h"
#include <iostream>

ForestCar::~ForestCar()
{
	if (_name) {
		delete[] _name;
		_name = NULL;
	}
}
