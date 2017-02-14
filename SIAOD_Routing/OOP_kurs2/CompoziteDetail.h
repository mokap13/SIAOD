#pragma once
#include "Detail.h"

class CompoziteDetail :
	public Detail
{
public:
	CompoziteDetail(std::string name);
	~CompoziteDetail();
	double getWeight();
	double getCost();
	unsigned long getMaufacturedTime();
private:
	std::list<Sptr> _details;
};

