#pragma once
#include "Detail.h"

class SingleDetail :
	public Detail
{
public:
	SingleDetail(std::string name);
	~SingleDetail();
	virtual double getWeight();
	virtual double getCost();
	virtual unsigned long getMaufacturedTime();
	void display();

	void setName(std::string name);
	void setMaterial(Material material);
	void setWeight(double weight);
	void setCost(double cost);
	void setManufacturedTime(double manufacturedTime);
};

