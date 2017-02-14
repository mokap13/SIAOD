#include "Detail.h"
#include <exception>

Detail::Detail(std::string name)
{
	_name = name;
}


Detail::~Detail()
{ 
}

void Detail::add(const Sptr &)
{
	throw new std::exception("Can't add to a SingleDetail");
}

void Detail::remove(const Sptr &)
{
	throw new std::exception("Can't remove to a SingleDetail");
}



std::string Detail::getName()
{
	return _name;
}

Material Detail::getMaterial()
{
	throw new std::exception("getMaterial not implementation");
}

double Detail::getWeight()
{
	throw new std::exception("getWeight not implementation");
}

double Detail::getCost()
{
	throw new std::exception("getCost not implementation");
}

unsigned long Detail::getMaufacturedTime()
{
	throw new std::exception("getMaufacturedTime not implementation");
}
