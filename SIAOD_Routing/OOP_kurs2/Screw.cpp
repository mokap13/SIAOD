#include "Screw.h"

Screw::Screw(std::string name) 
	:SingleDetail(name) {};

Screw::~Screw()
{
}

int Screw::getThreadPitch()
{
	return _threadPitch;
}

int Screw::getThreadDiameter()
{
	return _threadDiameter;
}

void Screw::setThreadPitch(int value)
{
	_threadPitch = value;
}

void Screw::setThreadDiameter(int value)
{
	_threadDiameter = value;
}




