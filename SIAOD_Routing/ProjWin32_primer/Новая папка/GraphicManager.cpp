#include "GraphicManager.h"

GraphicManager::GraphicManager()
{
	_window = GetConsoleWindow();	// получение ссылки на окно
	_deviceContext = GetDC(_window);			// получение контекста устройства
	RECT rect;
	GetClientRect(_window, &rect);
	_width = rect.right;
	_heigth = rect.bottom-120;
}


GraphicManager::~GraphicManager()
{
	ReleaseDC(_window, _deviceContext); // освобождение контекста устройства
	DeleteDC(_deviceContext);		  // удаление контекста устройства
}

void GraphicManager::setPen(int red, int green, int blue, int width)
{
	_pen = CreatePen(PS_INSIDEFRAME, width, RGB(red, green, blue));
	SelectObject(_deviceContext, _pen);
}

void GraphicManager::setBrush(int red, int green, int blue)
{
	_brush = CreateSolidBrush(RGB(red, green, blue));
	SelectObject(_deviceContext, _brush);
}

void GraphicManager::printPolygon(double * coordinates)
{
	int count = coordinates[0];
	POINT *_points = new POINT[count];
	for (int i = 1, j = 0; i < count * 2; i += 2, j++)
	{
		_points[j].x = (long)(coordinates[i] * 4.5f) + 5;
		_points[j].y = (long)_heigth - (long)(coordinates[i + 1] * 3.4f) + 10;
	}
	Polygon(_deviceContext, _points, count);
	
	if (_points) {
		delete[] _points;
		_points = NULL;
	}
}

void GraphicManager::printLine(double * coordinates)
{
	int count = coordinates[0];
	
	setCursor((long)(coordinates[1]* 4.5f)+5, (long)_heigth - (long)(coordinates[2]* 3.4f)+10);
	for (int i = 1, j = i+1; i < count * 2; i += 2, j++)
	{
		LineTo(_deviceContext, 
			(long)(coordinates[i] * 4.5f) + 5,
			(long)_heigth - (long)(coordinates[i + 1] * 3.4f) + 10);
	}
}

void GraphicManager::setCursor(int x, int y)
{
	MoveToEx(_deviceContext, x, y, NULL);
}

