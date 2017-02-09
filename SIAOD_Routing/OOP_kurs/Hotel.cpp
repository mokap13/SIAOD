#include "Hotel.h"
#include "Bill.h"

Hotel::Hotel(std::list<Room>* rooms)
{
	if (rooms) {
		_rooms = rooms;
	}
}

Hotel::~Hotel()
{
}

void Hotel::buyService(Client * client, list<Service>* services)
{
	Bill bill;
}

void Hotel::setClientForRoom(Client *client)
{
	Room *room;
	for (list<Room>::iterator i = _rooms->begin(); i != _rooms->end(); i++)
	{
		if (i->isEmpty()) {
			i->addClient(client);
		}
	}
}
