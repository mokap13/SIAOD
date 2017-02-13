#include "CompoziteDetail.h"

CompoziteDetail::~CompoziteDetail()
{
}

double CompoziteDetail::getWeight()
{
	_weight = 0;
	for (Sptr& sptr : _details)
	{
		_weight += sptr->getWeight();
	}
	return _weight;
}

double CompoziteDetail::getCost()
{
	_cost = 0;
	for (Sptr& sptr : _details)
	{
		_cost += sptr->getCost();
	}
	return _cost;
}

unsigned long CompoziteDetail::getMaufacturedTime()
{
	_manufacturedTime = 0;
	for (Sptr& sptr : _details)
	{
		_manufacturedTime += sptr->getMaufacturedTime();
	}
	return _manufacturedTime;
}
