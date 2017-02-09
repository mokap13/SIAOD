#include "Bill.h"
#include <list>
#include "Client.h"

using namespace std;

Bill::Bill()
{

}



Bill::~Bill()
{
	
}

void Bill::setClient(Client * client)
{
	_cost = 0;
	if (client) {
		_client = client;
	}
}

