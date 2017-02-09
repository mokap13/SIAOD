#pragma once
#include "Room.h"
#include <list>
using namespace std;

class Hotel
{
public:
	Hotel(std::list<Room>* rooms);
	~Hotel();
	void buyService(Client *client, list<Service>* services);

	void setClientForRoom(Client * client);

private:
	std::list<Room> *_rooms;
};

