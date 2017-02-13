#pragma once
#include <list>
using namespace std;

class FileManager
{
public:
	FileManager();
	~FileManager();
	void ReadFile(char* fileName);
	list<double*>* getCoordinates();
private:
	list<double*> _coordinates;
};

