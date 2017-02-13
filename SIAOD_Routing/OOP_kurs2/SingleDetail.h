#include "Detail.h"
#pragma once

class SingleDetail :
	public Detail
{
public:
	SingleDetail(char* name) :Detail(name) {};
	~SingleDetail();
	virtual double getWeight();
	virtual double getCost();
	virtual unsigned long getMaufacturedTime();
};

