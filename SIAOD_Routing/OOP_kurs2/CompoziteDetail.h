#pragma once
#include "Detail.h"

class CompoziteDetail :
	public Detail
{
public:
	CompoziteDetail(std::string name);
	~CompoziteDetail();
	void add(const Sptr & obj);
	void remove(const Sptr & obj);
	void display();

	double getWeight();
	double getCost();
	unsigned long getMaufacturedTime();
private:
	std::list<Sptr> _details;
	int count;
};

