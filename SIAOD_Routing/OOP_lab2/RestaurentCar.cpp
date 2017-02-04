#include "RestaurentCar.h"
#include <iostream>

RestaurentCar::~RestaurentCar()
{
	if (_name) {
		delete[] _name;
		_name = NULL;
	}
}
