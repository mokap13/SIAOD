#include "Bearing.h"


Bearing::Bearing() :SingleDetail("Bearing") {};

Bearing::~Bearing()
{
}

int Bearing::getDiameter()
{
	return _diameter;
}

void Bearing::setDiameter(int value)
{
	_diameter = value;
}
