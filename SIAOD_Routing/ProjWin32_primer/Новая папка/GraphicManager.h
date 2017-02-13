#pragma once
#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <iostream>

class GraphicManager
{
public:
	GraphicManager();
	~GraphicManager();
	void setPen(int red, int green, int blue, int width);
	void setBrush(int red, int green, int blue);
	void printPolygon(double* coordinates);
	void printLine(double* coordinates);
	void setCursor(int x, int y);
private:
	HWND _window;		// получение ссылки на окно
	HDC _deviceContext;			// получение контекста устройства
	HPEN _pen;			// декскриптор пера
	HBRUSH _brush;		// декскриптор кисти
	int _width;
	int _heigth;
};

