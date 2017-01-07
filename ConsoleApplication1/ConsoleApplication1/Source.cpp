/*� ��������� ������ ������� ���� �� ���������� �����, ������������ ����������, ������� ������� ��� � ����.
���������� ���������� ������ � �������� ���������� ��������� � �� ��������, � ����� ������ ������ � �������.
���� ���������� ���� ���-������ ��������,���� ��������.
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
//������ ���
void CEmployee::setName(char *name)
{
	this->mName = name;
}
//������ �������
void CEmployee::setAge(int age)
{
	this->mAge = age;
}
//������ ��������
void CEmployee::setSalary(int salary)
{
	this->mSalary = salary;
}
//�������� ������
int CEmployee::getIndex()
{
	return this->mIndex;
}
//������ ������
void CEmployee::setIndex(int index)
{
	this->mIndex = index;
}
//�������� ���
char * CEmployee::getFirstName()
{
	return this->mName;
}
//�������� �������
int CEmployee::getAge()
{
	return this->mAge;
}
//�������� �������� ��������
int CEmployee::getSalary()
{
	return this->mSalary;
}
//����� �� ����� ������ ���������
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
//������ ��������� �� ���������
void CEmployee::setNextEmployee(CEmployee *nextEmployee)
{
	this->nextEmployee = nextEmployee;
}
//������ ��������� �� �����������
void CEmployee::setPreviousEmployee(CEmployee *previousEmployee)
{
	this->previousEmployee = previousEmployee;
}
//�������� ��������� �� ���������� ���������
CEmployee * CEmployee::getNextEmplloyee()
{
	return this->nextEmployee;
}
//�������� ��������� �� ����������� ���������
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
//���������� ��������� � ������
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
//�������� �� �������
void CList::deleteByIndex(int index)
{
	if (!this->head && !this->tail) {
		cout << "������ ����!\n";
		system("pause");
		return;
	}
	else {
		CEmployee* tempEmployee = this->head;
		while (tempEmployee)
		{
			if (tempEmployee->getIndex() == index)
			{
				//���� �������� ������������ � ������
				if (tempEmployee == this->head && tempEmployee == this->tail) {
					this->head = this->tail = NULL;
				}
				//���� �������� � �������� ������
				else if (tempEmployee->getPreviousEmployee() && tempEmployee->getNextEmplloyee()) {
					tempEmployee->getPreviousEmployee()->setNextEmployee(tempEmployee->getNextEmplloyee());
					tempEmployee->getNextEmplloyee()->setPreviousEmployee(tempEmployee->getPreviousEmployee());
				}
				//���� �������� � ����� ������
				else if (!tempEmployee->getNextEmplloyee() && tempEmployee->getPreviousEmployee()) {
					tempEmployee->getPreviousEmployee()->setNextEmployee(nullptr);
					this->tail = tempEmployee->getPreviousEmployee();
				}
				//���� �������� � ������ ������
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
//����� ������ �� �����
void CList::display()
{
	CEmployee* tempEmployee = this->head;
	if (!head) {
		cout << "������ ����\n";
		system("pause");
	}
	else {
		cout << "������: ";
		cout.width(15);
		cout << "���: ";
		cout.width(20);
		cout << "�������:";
		cout.width(20);
		cout << "��������:" << endl;
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
		cout << "������ ����\n";
		system("pause");
	}
	else {
		cout << "������: ";
		cout.width(15);
		cout << "���: ";
		cout.width(20);
		cout << "�������:";
		cout.width(20);
		cout << "��������:" << endl;
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
	cout << "������� ��� ��������� ";
	cin >> name; cout << endl;
	cout << "������� ������� ��������� ";
	cin >> age; cout << endl;
	cout << "������� �������� ��������� ";
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
		cout << "1)����� ������ �� �����" << endl;
		cout << "2)����� ������ � �������� ������� �� �����" << endl;
		cout << "3)�������� ���������" << endl;
		cout << "4)������� ���������" << endl;
		cout << "�������� �������" << endl;
		cin >> choosedCommand; cout << endl;
		switch (choosedCommand)
		{
		case 1:
			cout << "������� ������ �� �����" << endl;
			employees.display();
			break;
		case 2:
			cout << "������� ������ � �������� ������� �� �����" << endl;
			employees.displayRevers();
			break;
		case 3:
			cout << "�������� ���������" << endl;
			employees.add(employees.CreateEmployee());
			break;
		case 4:
			cout << "������� ���������" << endl;
			cout << "������� ������ ���������" << endl;
			int choosedIndex;
			cin >> choosedIndex;
			employees.deleteByIndex(choosedIndex);
		default:
			break;
		}
	}
	system("pause");
}