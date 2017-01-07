class MyClass
{
public:
	MyClass(char*);
	~MyClass();

private:
	char* str;
	int a;
};

MyClass::MyClass(char* str)
{
	//this->str = new char[20];
	this->str = str;
}

MyClass::~MyClass()
{
	delete[] str;
}

void main(){
	MyClass *obj = new MyClass("abra");
	delete obj;
	
}