#pragma once
#include <list>
#include <memory>
#include <string>

enum Material
{
	steel,
	plastic,
	ceramics,
	glass,
	rubber
};
class Detail
{
public:
	Detail(std::string name);
	~Detail();
	
	typedef std::shared_ptr<Detail> Sptr;
	virtual void add(const Sptr&);
	virtual void remove(const Sptr&);
	virtual void display();

	std::string getName();
	virtual Material getMaterial();
	virtual double getWeight();
	virtual double getCost();
	virtual unsigned long getMaufacturedTime();
	
protected:
	std::string _name;
	Material _material;
	double _weight;
	double _cost;
	unsigned long _manufacturedTime;
};

