#include "Room.h"

Room::Room(int number)
{
	_number = number;
}


Room::~Room()
{
}

void Room::addClient(Client * client)
{
	if (client) {
		_client = client;
	}
}

void Room::removeClient()
{
	_client = nullptr;
}

bool Room::isEmpty()
{
	return _client == nullptr;
}

