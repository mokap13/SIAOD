#pragma once
#include "SingleDetail.h"
/*���������*/
class Bearing :
	public SingleDetail
{
public:
	Bearing();
	~Bearing();

	int getDiameter();
	void setDiameter(int value);
protected:
	int _diameter;
};

