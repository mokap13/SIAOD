#pragma once
#include "Client.h"
#include <list>
#include "Bill.h"

class Room
{
public:
	Room(int number);
	~Room();
	void addClient(Client* client);
	void removeClient();
	bool isEmpty();
private:
	Client *_client;
	int _number;
};

