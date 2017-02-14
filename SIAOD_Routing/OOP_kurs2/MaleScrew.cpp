#include "MaleScrew.h"


MaleScrew::MaleScrew() :
	Screw("MaleScrew") {};


MaleScrew::~MaleScrew()
{
}

int MaleScrew::getLength()
{
	return _length;
}

void MaleScrew::setLength(int value)
{
	_length = value;
}
