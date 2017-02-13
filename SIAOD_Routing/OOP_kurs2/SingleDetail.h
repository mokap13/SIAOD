#pragma once
#include "Detail.h"

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

