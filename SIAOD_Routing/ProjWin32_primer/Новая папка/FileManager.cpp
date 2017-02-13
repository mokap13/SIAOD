#include "FileManager.h"
#include <iostream>
#include <fstream>
#include <string>

using namespace std;

FileManager::FileManager()
{

}


FileManager::~FileManager()
{
	for (list<double*>::iterator i = _coordinates.begin(); i != _coordinates.end(); i++)
	{
		delete[] &i;
	}
}

void FileManager::ReadFile(char* fileName)
{
	ifstream file(fileName);
	if (!file) {
		throw new exception("file is null");
	}
	string str;
	while (getline(file, str)) { // пока не достигнут конец файла ложить очередную строку в переменную (s)
		if (str == (string)"Region  1") {
			getline(file, str);
			char* temp = new char[str.length()-2];
			for (int i = 2; i < str.length(); i++)
			{
				temp[i-2] = str[i];
			}
			int count = atoi(temp);
			delete temp;
			
			double* tempDouble = new double[count*2+1];
			for (int i = 1, j = 2; i < count*2 + 1; i += 2, j = i + 1)
			{
				tempDouble[0] = count;
				getline(file, str);
				int spaceIndex = 0;
				char* firstDouble = new char[str.length()];
				char* secondDouble = new char[str.length()];
				for (int k = 0; k < str.length(); k++)
				{
					if (str[k] == ' ') {
						spaceIndex = k;
						break;
					}
						
					firstDouble[k] = str[k];
				}
				for (int k = spaceIndex; k < str.length(); k++)
				{
					secondDouble[k- spaceIndex] = str[k+1];
				}
				tempDouble[i] = atof(firstDouble)/10;
				tempDouble[j] = atof(secondDouble)/10;
			}
			_coordinates.push_back(tempDouble);
		}
	}
	file.close();
}

list<double*>* FileManager::getCoordinates()
{
	return &_coordinates;
}
