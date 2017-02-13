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
	bool isLine = false;
	int count = 0;
	while (getline(file, str)) { // пока не достигнут конец файла ложить очередную строку в переменную (s)
		int lineCount = 0;
		
		if (str.substr(0,5) == (string)"Pline") {
			
			str = str.substr(6, str.length());
			lineCount = atof(str.c_str());
			isLine = true;
		}
		if (str == (string)"Region  1" || isLine) {	
			if (isLine) {
				count = lineCount;
			}
			else{
				getline(file, str);
				char* temp = new char[str.length() - 2];
				for (int i = 2; i < str.length(); i++)
				{
					temp[i - 2] = str[i];
				}
				count = atoi(temp);
				delete temp;
			}
			isLine = false;
			
			double* tempDouble = new double[count*2+1];
			for (int i = 1, j = 2; i < count*2 + 1; i += 2, j = i + 1)
			{
				tempDouble[0] = count;
				getline(file, str);
				int spaceIndex = 0;

				for (int k = 0; k < str.length(); k++)
				{
					if (str[k] == ' ') {
						spaceIndex = k;
						break;
					}	
				}
				tempDouble[i] = atof((str.substr(0,spaceIndex)).c_str())/10;
				tempDouble[j] = atof((str.substr(spaceIndex+1, str.length())).c_str()) / 10;
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
