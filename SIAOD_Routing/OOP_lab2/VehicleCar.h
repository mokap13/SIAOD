#pragma once
#include "Car.h"
class VehicleCar :
	public Car
{
public:
	VehicleCar(int weight) :Car("����������",weight) {};
	~VehicleCar();
};

