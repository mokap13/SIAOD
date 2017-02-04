#pragma once
#include "Car.h"
class ForestCar :
	public Car
{
public:
	ForestCar(int weight) :Car("Лес", weight) {};
	~ForestCar();
};

