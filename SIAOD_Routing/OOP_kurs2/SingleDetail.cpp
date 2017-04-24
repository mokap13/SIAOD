#include "SingleDetail.h"
#include <iostream>
using namespace std;

SingleDetail::SingleDetail(std::string name)
	:Detail(name) {
	_weight = 0;
};

SingleDetail::~SingleDetail()
{
}

double SingleDetail::getWeight()
{
	return _weight;
}

double SingleDetail::getCost()
{
	return _cost;
}

unsigned long SingleDetail::getMaufacturedTime()
{
	return _manufacturedTime;
}

void SingleDetail::display()
{
	cout << " " << _name;	
}

void SingleDetail::setName(std::string name)
{
	_name = name;
}

void SingleDetail::setMaterial(Material material)
{
	_material = material;
}

void SingleDetail::setWeight(double weight)
{
	_weight = weight;
}

void SingleDetail::setCost(double cost)
{
	_cost = cost;
}

void SingleDetail::setManufacturedTime(double manufacturedTime)
{
	_manufacturedTime = manufacturedTime;
}
