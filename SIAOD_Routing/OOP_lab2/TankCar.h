#pragma once
#include "Car.h"
class TankCar :
	public Car
{
public:
	TankCar(int weigth) :Car("��������",weigth) {};
	~TankCar();
};

