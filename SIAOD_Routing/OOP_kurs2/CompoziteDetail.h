#pragma once
#include "Detail.h"

class CompoziteDetail :
	public Detail
{
public:
	CompoziteDetail(char* name) :Detail(name) {};
	~CompoziteDetail();
	virtual double getWeight();
	virtual double getCost();
	virtual unsigned long getMaufacturedTime();
private:
	std::list<Sptr> _details;
};

