#pragma once
class Person
{
public:
	Person();
	Person(char *name, char *familia, char *otchestvo, int age);
	~Person();

	void setName(char*);
	void setFamilia(char*);
	void setOtchestvo(char*);
	void setAge(int);

	char* getName();
	char* getFamilia();
	char* getOtchestvo();
	int getAge();

	void printToConsole();
private:
	char* _name;
	char* _familia;
	char* _otchestvo;
	int _age;

	void initMemory();
};

