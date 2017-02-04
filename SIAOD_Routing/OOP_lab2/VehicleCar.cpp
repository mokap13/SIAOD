#include "VehicleCar.h"
#include <iostream>

VehicleCar::~VehicleCar()
{
	if (_name) {
		delete[] _name;
		_name = NULL;
	}
}
