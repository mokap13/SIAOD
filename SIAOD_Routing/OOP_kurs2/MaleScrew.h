#pragma once
#include "Screw.h"
/*����*/
class MaleScrew :
	public Screw
{
public:
	MaleScrew();
	~MaleScrew();

	int getLength();
	void setLength(int value);
protected:
	int _length;
};

