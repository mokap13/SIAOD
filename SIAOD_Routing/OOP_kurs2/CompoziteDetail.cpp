#include "CompoziteDetail.h"

CompoziteDetail::CompoziteDetail(std::string name) :Detail(name) {};

CompoziteDetail::~CompoziteDetail()
{
}

double CompoziteDetail::getWeight()
{
	int childrensWeight = 0;
	for (Sptr& sptr : _details)
	{
		childrensWeight += sptr->getWeight();
	}
	return _weight+childrensWeight;
}

double CompoziteDetail::getCost()
{
	int childrensCost = 0;
	for (Sptr& sptr : _details)
	{
		childrensCost += sptr->getCost();
	}
	return _cost + childrensCost;
}

unsigned long CompoziteDetail::getMaufacturedTime()
{
	int childrensManufacturedTime = 0;
	for (Sptr& sptr : _details)
	{
		childrensManufacturedTime += sptr->getMaufacturedTime();
	}
	return _manufacturedTime + childrensManufacturedTime;
}
