#pragma once
class Car
{
public:
	Car(char* name, int weight);
	virtual ~Car();

	int getWeight();
	char* getName();
protected:
	char* _name;
	int _weight;
};

