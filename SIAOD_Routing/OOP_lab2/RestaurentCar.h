#pragma once
#include "Car.h"
class RestaurentCar :
	public Car
{
public:
	RestaurentCar(int weight) : Car("Ресторан", weight) {};
	~RestaurentCar();
};

