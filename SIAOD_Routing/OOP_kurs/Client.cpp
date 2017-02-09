#include "Client.h"

#include <iostream>

#define NAME_SIZE 20

using namespace std;

Client::Client(char * name)
{
	_name = new char[NAME_SIZE];
	if (!_name) {
		cout << "_name Memory error\n";
	}
	if (strlen(name) >= NAME_SIZE){
		cout << "_name Size errro\n";
	return;
	}
	strcpy_s(_name,NAME_SIZE, name);
}

Client::~Client()
{
	if (_name) {
		delete[] _name;
	}
}
