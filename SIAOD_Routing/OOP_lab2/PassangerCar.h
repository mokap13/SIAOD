#pragma once
#include "Car.h"
class PassangerCar :
	public Car
{
public:
	PassangerCar(int weight) :Car("Пассажиры", weight) {};
	~PassangerCar();
};

