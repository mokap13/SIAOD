#pragma once
#include "Car.h"
class ForestCar :
	public Car
{
public:
	ForestCar(int weight) :Car("���", weight) {};
	~ForestCar();
};

