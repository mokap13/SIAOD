#include <iostream>

using namespace std;

class Employee
{
public:
	Employee(char*, int, int);
	~Employee();
	void SetName(char*);
	void SetAge(int);
	void SetSalary(int);
	int GetIndex();
	char* GetFirstName();
	int GetAge();
	int GetSalary();
	void Display();
	void SetNextEmployee(Employee*);
	void SetPreviousEmployee(Employee*);
	Employee* GetNextEmplloyee();
	Employee* GetPreviousEmployee();
private:
	char* name;
	int age;
	int salary;
	int index;
	Employee* nextEmployee;
	Employee* previousEmployee;
};

Employee::Employee(char *name, int age, int salary)
{
	this->name = new char[20];

	this->name = name;
	this->age = age;
	this->salary = salary;

	if (this->previousEmployee) {
		this->index = previousEmployee->GetIndex() + 1;
	}
	else {
		index = 0;
	}
}

Employee::~Employee()
{
	delete[] this->name;
}

void Employee::SetName(char *name)
{
	this->name = name;
}

void Employee::SetAge(int age)
{
	this->age = age;
}

void Employee::SetSalary(int salary)
{
	this->salary = salary;
}

int Employee::GetIndex()
{
	return this->index;
}

char * Employee::GetFirstName()
{
	return this->name;
}

int Employee::GetAge()
{
	return this->age;
}

int Employee::GetSalary()
{
	return this->salary;
}

void Employee::Display()
{
	cout.width(12);
	cout << this->name;
	cout.width(5);
	cout << this->age;
	cout.width(7);
	cout << this->salary << endl;
}

void Employee::SetNextEmployee(Employee *nextEmployee)
{
	this->nextEmployee = nextEmployee;
}

void Employee::SetPreviousEmployee(Employee *previousEmployee)
{
	this->previousEmployee = previousEmployee;
}

Employee * Employee::GetNextEmplloyee()
{
	return this->nextEmployee;
}

Employee * Employee::GetPreviousEmployee()
{
	return this->previousEmployee;
}
////////////////////////////////////////////////////////////////////////////////
class List
{
public:
	List();
	~List();
	void Add(Employee*);
	void DeleteByIndex(int);
	void Display();
private:
	Employee* head;
	Employee* tail;
};

List::List()
{
	this->head = NULL;
	this->tail = NULL;
}

List::~List()
{
}

void List::Add(Employee *newEmployee)
{
	if (!this->head) {
		this->head = this->tail = newEmployee;
		newEmployee->SetNextEmployee(nullptr);
		newEmployee->SetPreviousEmployee(nullptr);
	}
	else {
		newEmployee->SetPreviousEmployee(this->tail);
		tail->SetNextEmployee(newEmployee);
		this->tail = newEmployee;
		newEmployee->SetNextEmployee(nullptr);
	}
}

void List::DeleteByIndex(int index)
{
	if (!this->head && !this->tail) {
		cout << "Список пуст!\n";
		system("pause");
		return;
	}
	else {
		Employee* tempEmployee = this->head;
		while (tempEmployee)
		{
			if (tempEmployee->GetIndex() == index)
			{
				if (tempEmployee == this->head && tempEmployee == this->tail) {
					this->head = this->tail = NULL;
				}
				else if (tempEmployee->GetPreviousEmployee() && tempEmployee->GetNextEmplloyee()) {
					tempEmployee->GetPreviousEmployee()->SetNextEmployee(tempEmployee->GetNextEmplloyee());
					tempEmployee->GetNextEmplloyee()->SetPreviousEmployee(tempEmployee->GetPreviousEmployee());
				}
				else if (!tempEmployee->GetNextEmplloyee() && tempEmployee->GetPreviousEmployee()) {
					tempEmployee->GetPreviousEmployee()->SetNextEmployee(nullptr);
					this->tail = tempEmployee->GetPreviousEmployee();
				}
				else {
					tempEmployee->GetNextEmplloyee()->SetPreviousEmployee(nullptr);
					this->head = tempEmployee->GetNextEmplloyee();
				}
				delete tempEmployee;
			}
			else {
				tempEmployee = tempEmployee->GetNextEmplloyee();
			}
		}
	}
}

void List::Display()
{
	Employee* tempEmployee = this->head;
	if (!head) {
		cout << "Список пуст\n";
		system("pause");
	}
	else {
		cout.width(12);
		cout << "Имя: ";
		cout.width(5);
		cout << "Возраст: ";
		cout.width(7);
		cout << "Зарплата: " << endl;
		while (tempEmployee)
		{
			tempEmployee->Display();
			tempEmployee = tempEmployee->GetNextEmplloyee();
		}
	}
}


void main() {
	setlocale(LC_CTYPE, "rus");

	List employees;
	employees.Add(new Employee("Mihail", 22, 23400));
	employees.Add(new Employee("Ivan", 23, 22300));
	employees.Add(new Employee("Maria", 26, 27200));
	employees.Add(new Employee("Evgeniy", 20, 19500));
	employees.Add(new Employee("Alexander", 32, 34000));

	employees.Display();

	system("pause");
}



