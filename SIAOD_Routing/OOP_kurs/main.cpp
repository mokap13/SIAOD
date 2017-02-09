#include "Bill.h"
#include "Client.h"
#include "Hotel.h"
#include "Room.h"
#include <list>
#define ROOMS_COUNT 25
using namespace std;

void main() {
	list<Room> rooms;
	for (int i = 0; i < ROOMS_COUNT; i++)
	{
		rooms.push_back(*new Room(i+1));
	}
	

	Hotel hotel(&rooms);

	Client clientA("Roman");
	Client clientB("Mihail");


	
	system("pause");
}