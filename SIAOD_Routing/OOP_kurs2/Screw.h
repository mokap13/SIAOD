#pragma once
#include "SingleDetail.h"
/*����� ���������*/
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
	/*��� ������*/
	int _threadPitch;
	int _threadDiameter;
};

