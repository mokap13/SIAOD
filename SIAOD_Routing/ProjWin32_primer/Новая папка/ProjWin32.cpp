#include <stdio.h>
#include <conio.h>
#include <iostream>
#include "GraphicManager.h"
#include "FileManager.h"

using namespace std;

double mas[19] = {9, 
66.37962931, 125,
53.0416681, 125,
41.56481894, 125,
0.31018521, 125,
0.31018521, 124.076352,
0.31018521,108.990144,
0.31018521, 103.75616,
59.86574566, 92.980288,
66.37962931, 125};


void main()
{
	system("mode con cols=120 lines=60");

	GraphicManager graphicManagerRegions;
	GraphicManager graphicManagerHouses;
	

	FileManager fileManager;
	fileManager.ReadFile("Regions.MIF");
	
	list<double*>* coordinates = fileManager.getCoordinates();

	graphicManagerRegions.setBrush(0xcc, 0x0, 0xff);
	for (list<double*>::iterator i = coordinates->begin(); i != coordinates->end(); i++)
	{
		graphicManagerRegions.printPolygon(*i);
	}

	fileManager.ReadFile("Houses.MIF");

	graphicManagerHouses.setBrush(0xff, 0xff,0);
	for (list<double*>::iterator i = coordinates->begin(); i != coordinates->end(); i++)
	{
		graphicManagerHouses.printPolygon(*i);
	}

	fileManager.ReadFile("Street.MIF");

	graphicManagerHouses.setBrush(0, 0xff, 0xff);
	for (list<double*>::iterator i = coordinates->begin(); i != coordinates->end(); i++)
	{
		graphicManagerHouses.printPolygon(*i);
	}

	coordinates->clear();
	system("pause");
}







/*
void RendPolgKvart(int wid, int heg)
{
	POINT poly[STB_k];
	int i,j,k;

	for(i=0; i<STR_k; i++)
	{
		if(i>=0 && i <=3)
		{
			hBrush = CreateSolidBrush(RGB(100,200,100)); //задание сплошной кисти, закрашенной цветом RGB
			SelectObject(dc, hBrush); //кисть активна  
		}
		else
		{
			hBrush = CreateSolidBrush(RGB(150,150,150)); //задание сплошной кисти, закрашенной цветом RGB
			SelectObject(dc, hBrush); //кисть активна  
		}
		for(j=0; j<STB_k; j++)
		{
			poly[j].x = 0;
			poly[j].y = 0;
		}
		k = (int)koor_kvart[i][0];
		for(j=1; j<=k; j++)
		{
			poly[j-1].x = (long)(koor_kvart[i][j*2-1]*4)+30;
			poly[j-1].y = (long)heg - (long)(koor_kvart[i][j*2]*3)-25;
		}
		Polygon(dc, poly , k);
	}
}

void RendPolgDoma(int wid, int heg)
{
	POINT poly[STB_d];
	int i,j,k;

	hBrush = CreateSolidBrush(RGB(200,200,0));	//задание сплошной кисти, закрашенной цветом RGB
	SelectObject(dc, hBrush);						//кисть активна  
	for(i=0; i<STR_d; i++)
	{
		for(j=0; j<STB_d; j++)
		{
			poly[j].x = 0;
			poly[j].y = 0;
		}
		k = (int)koor_doma[i][0];
		for(j=1; j<=k; j++)
		{
			poly[j-1].x = (long)(koor_doma[i][j*2-1]*4)+30;
			poly[j-1].y = (long)heg - (long)(koor_doma[i][j*2]*3)-25;
		}
		Polygon(dc, poly , k);
	}
}
*/

//---------------------------------------------------------------------------------
