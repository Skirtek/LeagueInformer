// delta.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <math.h>

void liniowa(float b, float c)
{
	using namespace std;
	if (b == 0)
	{
		if (c == 0)
			cout << "Tożsamość" << endl; // a=0 i b==0 -> skoro c=0 to 0=0
		else
			cout << "Sprzeczność" << endl; // jeżeli a i b to 0 jak wyżej to np. 2 = 0 to sprzeczność
	}
	else
	{
		if (c == 0)
			cout << "x = 0"; // skoro a=0 i c=0 -> bx=0 -> x/b=0 , a to jest prawdziwe tylko gdy x == 0
		else
		{
			float x;
			x = (c*(-1)) / b; // skoro a=0 -> bx + c == 0 -> x=(-c)/b
			cout << "x = " << x;
		}
	}
}

void kwadratowa(float a, float b, float c)
{
	using namespace std;
	if (b == 0)
	{
		if (c == 0)
		{
			cout << "x = 0"; //b=0 i c=0 -> ax^2 == 0 -> x==0
		}
		else
		{
			float x;
			x = sqrtf((c*(-1)) / a); //b==0 -> ax^2 + c == 0  ->  x = sqrt(-c/a)
			cout << "x = " << x;
		}
	}
	else
	{
		if (c == 0)
		{
			float x;
			x = ((b*(-1)) / a); //c=0 -> ax^2 + bx = 0  ->  ax + b = 0 -> x = -b/a
			cout << "x = " << x;
		}
		else
		{
			float delta, x1, x2; //nic nie rowna sie zero liczymy gowniana delte
			delta = b * b - 4 * a*c;
			if (delta < 0)
			{
				delta = delta * (-1);
				x1 = (b*(-1) - sqrtf(delta)) / 2 * a;
				x2 = (b*(-1) + sqrtf(delta)) / 2 * a;
				cout << "x1 = " << x1 << "i" << endl;
				cout << "x2 = " << x2 << "i" << endl;
			}
			else
			{
				x1 = (b*(-1) - sqrtf(delta)) / 2 * a;
				x2 = (b*(-1) + sqrtf(delta)) / 2 * a;
				cout << "x1 = " << x1 << endl;
				cout << "x2 = " << x2 << endl;
			}
		}
	}
}

int main()
{
	using namespace std;
	float a, b, c;
	cout << "Podaj a, b i c:" << endl;
	cout << "a: "; cin >> a;
	cout << "b: "; cin >> b;
	cout << "c: "; cin >> c;
	// ax^2+bx+c=0
	if (a == 0)
		liniowa(b, c); //Jeżeli a=0 to funkcja jest liniowa -> GOTO funkcja "liniowa"
	else
		kwadratowa(a, b, c);
	cout << endl << endl;
	system("pause");
    return 0;
}