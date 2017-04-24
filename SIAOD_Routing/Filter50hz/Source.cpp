#include <iostream>
#define _USE_MATH_DEFINES
#include <math.h>


using namespace std;

/**************************************************************
WinFilter version 0.8
http://www.winfilter.20m.com
akundert@hotmail.com

Filter type: Low Pass
Filter model: Bessel
Filter order: 11
Sampling Frequency: 200 Hz
Cut Frequency: 25.000000 Hz
Coefficents Quantization: 16-bit

Z domain Zeros
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000
z = -1.000000 + j 0.000000

Z domain Poles
z = 0.139920 + j -0.331796
z = 0.139920 + j 0.331796
z = 0.718752 + j -0.429961
z = 0.718752 + j 0.429961
z = 0.332062 + j -0.558016
z = 0.332062 + j 0.558016
z = 0.051669 + j -0.000000
z = 0.821855 + j -0.159648
z = 0.821855 + j 0.159648
z = 0.533842 + j -0.646603
z = 0.533842 + j 0.646603
***************************************************************/
#define NCoef 11
#define DCgain 16384

__int16 iir(__int16 NewSample) {
	__int16 ACoef[NCoef + 1] = {
		45,
		499,
		2495,
		7486,
		14973,
		20963,
		20963,
		14973,
		7486,
		2495,
		499,
		45
	};

	__int16 BCoef[NCoef + 1] = {
		1024,
		-5268,
		12979,
		-20044,
		21426,
		-16567,
		9421,
		-3928,
		1173,
		-238,
		29,
		-1
	};

	static __int32 y[NCoef + 1]; //output samples
								 //Warning!!!!!! This variable should be signed (input sample width + Coefs width + 11 )-bit width to avoid saturation.

	static __int16 x[NCoef + 1]; //input samples
	int n;

	//shift the old samples
	for (n = NCoef; n>0; n--) {
		x[n] = x[n - 1];
		y[n] = y[n - 1];
	}

	//Calculate the new output
	x[0] = NewSample;
	y[0] = ACoef[0] * x[0];
	for (n = 1; n <= NCoef; n++)
		y[0] += ACoef[n] * x[n] - BCoef[n] * y[n];

	y[0] /= BCoef[0];

	return y[0] / DCgain;
}



void main() {
	int a[1500];
	for (size_t i = 0; i < 1500; i++)
	{
		a[i] = sin(2*M_PI*0.05*i)*500;
	}
	for (size_t i = 0; i < 1500; i++)
	{
		cout << "[" << a[i] << "]  ";
		a[i] = iir(a[i]);
		cout << a[i] ;
		cout << "\n";
	}

	getchar();
}