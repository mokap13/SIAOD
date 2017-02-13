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
	HWND _window;		// ��������� ������ �� ����
	HDC _deviceContext;			// ��������� ��������� ����������
	HPEN _pen;			// ����������� ����
	HBRUSH _brush;		// ����������� �����
	int _width;
	int _heigth;
};

