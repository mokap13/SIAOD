/*К сожелению старых заданий пока не получилось найти, форматировал винтчестер, поэтому написал код с нуля.
Реализовал двусвязный список с функцией добавления элементов и их удаления, а также вывода списка в консоль.
Если необходимо могу что-нибудь добавить,либо изменить.
*/

#include <iostream>

using namespace std;

class CEmployee
{
public:
	CEmployee(char*, int, int);
	~CEmployee();
	void setName(char*);
	void setAge(int);
	void setSalary(int);
	int getIndex();
	void setIndex(int);
	char* getFirstName();
	int getAge();
	int getSalary();
	void display();
	void setNextEmployee(CEmployee*);
	void setPreviousEmployee(CEmployee*);
	CEmployee* getNextEmplloyee();
	CEmployee* getPreviousEmployee();
private:
	char* mName;
	int mAge;
	int mSalary;
	int mIndex;
	CEmployee* nextEmployee;
	CEmployee* previousEmployee;
};

CEmployee::CEmployee(char *name, int age, int salary)
{
	this->mName = name;
	this->mAge = age;
	this->mSalary = salary;
}

CEmployee::~CEmployee()
{
	if (this->mName)
		delete[] this->mName;
}
//Задать имя
void CEmployee::setName(char *name)
{
	this->mName = name;
}
//Задать возраст
void CEmployee::setAge(int age)
{
	this->mAge = age;
}
//Задать зарплату
void CEmployee::setSalary(int salary)
{
	this->mSalary = salary;
}
//Получить индекс
int CEmployee::getIndex()
{
	return this->mIndex;
}
//Задать индекс
void CEmployee::setIndex(int index)
{
	this->mIndex = index;
}
//Получить имя
char * CEmployee::getFirstName()
{
	return this->mName;
}
//Получить возраст
int CEmployee::getAge()
{
	return this->mAge;
}
//Получить параметр зарплаты
int CEmployee::getSalary()
{
	return this->mSalary;
}
//Вывод на экран одного работника
void CEmployee::display()
{
	cout << this->mIndex;
	cout.width(20);
	cout << this->mName;
	cout.width(20);
	cout << this->mAge;
	cout.width(20);
	cout << this->mSalary << endl;
}
//Задать указатель на следущего
void CEmployee::setNextEmployee(CEmployee *nextEmployee)
{
	this->nextEmployee = nextEmployee;
}
//Задать указатель на предыдущего
void CEmployee::setPreviousEmployee(CEmployee *previousEmployee)
{
	this->previousEmployee = previousEmployee;
}
//Получить указатель на следующего работника
CEmployee * CEmployee::getNextEmplloyee()
{
	return this->nextEmployee;
}
//Получить указатель на предыдущего работника
CEmployee * CEmployee::getPreviousEmployee()
{
	return this->previousEmployee;
}

class CList
{
public:
	CList();
	~CList();
	void add(CEmployee*);
	void deleteByIndex(int);
	void display();
	void displayRevers();
	CEmployee* CreateEmployee();
private:
	CEmployee* head;
	CEmployee* tail;
};

CList::CList()
{
	this->head = nullptr;
	this->tail = nullptr;
}

CList::~CList()
{

}
//Добавление работника в список
void CList::add(CEmployee *newEmployee)
{
	if (!this->head) {
		this->head = this->tail = newEmployee;
		newEmployee->setNextEmployee(nullptr);
		newEmployee->setPreviousEmployee(nullptr);
		newEmployee->setIndex(0);
	}
	else {
		newEmployee->setPreviousEmployee(this->tail);
		tail->setNextEmployee(newEmployee);
		this->tail = newEmployee;
		newEmployee->setNextEmployee(nullptr);
		newEmployee->setIndex(newEmployee->getPreviousEmployee()->getIndex() + 1);
	}
}
//Удаление по индексу
void CList::deleteByIndex(int index)
{
	if (!this->head && !this->tail) {
		cout << "Список пуст!\n";
		system("pause");
		return;
	}
	else {
		CEmployee* tempEmployee = this->head;
		while (tempEmployee)
		{
			if (tempEmployee->getIndex() == index)
			{
				//Если работник единственный в списке
				if (tempEmployee == this->head && tempEmployee == this->tail) {
					this->head = this->tail = NULL;
				}
				//Если работник в середине списка
				else if (tempEmployee->getPreviousEmployee() && tempEmployee->getNextEmplloyee()) {
					tempEmployee->getPreviousEmployee()->setNextEmployee(tempEmployee->getNextEmplloyee());
					tempEmployee->getNextEmplloyee()->setPreviousEmployee(tempEmployee->getPreviousEmployee());
				}
				//Если работник в конце списка
				else if (!tempEmployee->getNextEmplloyee() && tempEmployee->getPreviousEmployee()) {
					tempEmployee->getPreviousEmployee()->setNextEmployee(nullptr);
					this->tail = tempEmployee->getPreviousEmployee();
				}
				//Если работник в начале списка
				else {
					tempEmployee->getNextEmplloyee()->setPreviousEmployee(nullptr);
					this->head = tempEmployee->getNextEmplloyee();
				}
				delete tempEmployee;
				return;
			}
			else {
				tempEmployee = tempEmployee->getNextEmplloyee();
			}
		}
	}
}
//Вывод списка на экран
void CList::display()
{
	CEmployee* tempEmployee = this->head;
	if (!head) {
		cout << "Список пуст\n";
		system("pause");
	}
	else {
		cout << "Индекс: ";
		cout.width(15);
		cout << "Имя: ";
		cout.width(20);
		cout << "Возраст:";
		cout.width(20);
		cout << "Зарплата:" << endl;
		while (tempEmployee)
		{
			tempEmployee->display();
			tempEmployee = tempEmployee->getNextEmplloyee();
		}
		cout << "\n\n\n";
		system("pause");
	}
}

void CList::displayRevers()
{
	CEmployee* tempEmployee = this->tail;
	if (!head) {
		cout << "Список пуст\n";
		system("pause");
	}
	else {
		cout << "Индекс: ";
		cout.width(15);
		cout << "Имя: ";
		cout.width(20);
		cout << "Возраст:";
		cout.width(20);
		cout << "Зарплата:" << endl;
		while (tempEmployee)
		{
			tempEmployee->display();
			tempEmployee = tempEmployee->getPreviousEmployee();
		}
		cout << "\n\n\n";
		system("pause");
	}
}

CEmployee * CList::CreateEmployee()
{
	char* name = new char[20];
	int age;
	int salary;
	cout << "Введите имя работника ";
	cin >> name; cout << endl;
	cout << "Введите возраст работника ";
	cin >> age; cout << endl;
	cout << "Введите зарплату работника ";
	cin >> salary; cout << endl;

	return new CEmployee(name, age, salary);
}

void main() {
	setlocale(LC_CTYPE, "rus");

	CList employees;
	int choosedCommand;

	while (true)
	{
		system("CLS");
		cout << "1)Вывод списка на экран" << endl;
		cout << "2)Вывод списка в обратном порядке на экран" << endl;
		cout << "3)Добавить работника" << endl;
		cout << "4)Удалить работника" << endl;
		cout << "Выберите команду" << endl;
		cin >> choosedCommand; cout << endl;
		switch (choosedCommand)
		{
		case 1:
			cout << "Вывести список на экран" << endl;
			employees.display();
			break;
		case 2:
			cout << "Вывесте список в обратном порядке на экран" << endl;
			employees.displayRevers();
			break;
		case 3:
			cout << "Добавить работника" << endl;
			employees.add(employees.CreateEmployee());
			break;
		case 4:
			cout << "Удалить работника" << endl;
			cout << "Введите индекс работника" << endl;
			int choosedIndex;
			cin >> choosedIndex;
			employees.deleteByIndex(choosedIndex);
		default:
			break;
		}
	}
	system("pause");
}