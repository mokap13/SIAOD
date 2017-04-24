#include "CompoziteDetail.h"
#include <iostream>
#include <Windows.h>

using namespace std;

CompoziteDetail::CompoziteDetail(std::string name) :Detail(name) { _weight = 0; };

CompoziteDetail::~CompoziteDetail()
{
}

void CompoziteDetail::add(const Sptr & obj)
{
	_details.push_back(obj);
}

void CompoziteDetail::remove(const Sptr & obj)
{
	_details.remove(obj);
}

void CompoziteDetail::display()
{
	cout << _name << "("<< getWeight() << ")";
	cout << "\n";
	for (Sptr& sptr : _details)
	{
		sptr->display();
		cout << "\n";
	}
}

double CompoziteDetail::getWeight()
{
	for (Sptr& sptr : _details)
	{
		_weight += sptr->getWeight();
	}
	return _weight;
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
