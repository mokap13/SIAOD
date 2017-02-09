#pragma once
#include <list>
#include "Client.h"

enum Service
{
	OneRoom = 1500,
	TV = 150,
	Internet = 170,
	Lunch = 100
};

class Bill
{
public:
	Bill();
	~Bill();
	void setClient(Client *client);
private:
	Service *_services;
	Client *_client;
	int _cost;
};

