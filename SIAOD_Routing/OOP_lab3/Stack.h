#pragma once
#include <ostream>

template <class T>
struct StackNode {
	StackNode(T data, StackNode<T>* next)
		: _data(data), _prev(next) {}
	StackNode* _prev;
	T _data;
};

template <class T>
class Stack
{
public:

Stack() : _count(0), _topNode(NULL) {
}

~Stack() {
	while (!isEmpty()) {
		pop();
	}
}

void push(T data) {
	StackNode<T>* newNode = new StackNode<T>(data, _topNode);
	_topNode = newNode;
	_count++;
}

T pop() {
	if (!isEmpty()) {
		StackNode<T>* popped = _topNode;
		T poppedData = popped->_data;
		_topNode = popped->_prev;
		_count--;
		delete popped;
		return poppedData;
	}

	throw new std::exception("Stack is empty!");
}

bool isEmpty() {
	return _count == 0;
}

void printConsole() const {
	StackNode<T>* tempTop = _topNode;
	while (tempTop != NULL) {
		std::cout << tempTop->_data << endl;
		tempTop = tempTop->_prev;
	}
}

int count() const {
		return _count;
	}

private:
	StackNode<T>* _topNode;
	int _count;

};