#pragma once
#include "SingleDetail.h"
/*Êכאסס נוחüבמגûץ*/
class Screw :
	public SingleDetail
{
public:
	Screw(std::string name);
	~Screw();
	
	int getThreadPitch();
	int getThreadDiameter();

	void setThreadPitch(int value);
	void setThreadDiameter(int value);
protected:
	/*״אד נוחüבû*/
	int _threadPitch;
	int _threadDiameter;
};

