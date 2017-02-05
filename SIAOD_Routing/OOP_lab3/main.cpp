#include "Stack.h"
#include <iostream>

using namespace std;

int main() {
	Stack<double> testDoubleValues;
	testDoubleValues.push(2.34);
	testDoubleValues.push(421.1325);
	testDoubleValues.push(251.244);
	testDoubleValues.push(636.123);

	testDoubleValues.printConsole();

	testDoubleValues.pop();
	testDoubleValues.pop();

	cout << "*****************\n";
	testDoubleValues.printConsole();
	cout << "*****************\n";
	cout << "*****************\n";
	/*Пример с типо char*/
	Stack<char> testCharValues;
	testCharValues.push('1');
	testCharValues.push('2');
	testCharValues.push('3');
	testCharValues.push('4');

	testCharValues.printConsole();

	testCharValues.pop();
	cout << "*****************\n";
	testCharValues.printConsole();

	system("pause");
}