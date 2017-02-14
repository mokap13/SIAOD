#include "SingleDetail.h"


SingleDetail::SingleDetail(std::string name)
	:Detail(name) {};

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