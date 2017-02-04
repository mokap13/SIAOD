#include "TankCar.h"
#include <iostream>

TankCar::~TankCar()
{
	if (_name) {
		delete[] _name;
		_name = NULL;
	}
}
