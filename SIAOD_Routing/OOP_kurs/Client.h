#pragma once
#include "Bill.h"

class Client
{
public:
	Client(char* name);
	~Client();
private:
	char* _name;
};

