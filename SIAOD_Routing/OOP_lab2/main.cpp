#include "Car.h"
#include "ForestCar.h"
#include "PassangerCar.h"
#include "RestaurentCar.h"
#include "TankCar.h"
#include "VehicleCar.h"
#include <random>
#include <list>
#include <ctime>
#include <iostream>

using namespace std;

void main() {
	setlocale(LC_ALL, "rus");
	srand(time(0));
	
	int carsCount = rand()%20 + 2;
	long carWeight = 0;

	list<Car> cars;
	for (int i = 0; i < carsCount; i++)
	{
		
		carWeight = (rand()*256) % 50000 + 20000;
		Car *car = new Car("0",0);
		switch (rand()%5)
		{
		case 0:
			car = dynamic_cast<Car*>(new TankCar(carWeight));
			break;
		case 1:
			car = dynamic_cast<Car*>(new ForestCar(carWeight));
			break;
		case 2:
			car = dynamic_cast<Car*>(new PassangerCar(carWeight));
			break;
		case 3:
			car = dynamic_cast<Car*>(new RestaurentCar(carWeight));
			break;
		case 4:
			car = dynamic_cast<Car*>(new VehicleCar(carWeight));
			break;
		default:
			break;
		}
		cars.push_back(*car);
	}

	long weight = 0;
	for (list<Car>::iterator i = cars.begin(); i != cars.end(); i++)
	{
		weight += i->getWeight();
	}
	cout << "Вес :" << weight << " кг" << endl;
	for (list<Car>::iterator i = cars.begin(); i != cars.end(); i++)
	{
		cout << "[" << i->getName() << "].";
	}
	system("pause");
}