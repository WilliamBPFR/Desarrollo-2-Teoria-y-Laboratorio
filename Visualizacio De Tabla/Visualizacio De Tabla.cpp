// Visualizacio De Tabla.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <stdio.h>
#include <conio.h>

using namespace std;

int juego()
{
	cout << "--------------------------Bienvenido a MasterMind--------------------------\n";
	cout << " Vaya introduciendo numero por numero, uno por uno. Luego de escribir un numero\n";
	cout << " de la clave de enter para introducir el otro hasta en introducir los 4 numeros\n";
	cout << " por intento. No introducir mas de 1 numero por columna.\n\n";
	cout << "|---------------------------------------------------------------------------|\n";
	cout << "| Intentos |  A  |  B  |  C  |  D  |  || | Intentos |  A  |  B  |  C  |  D  |\n";

	for (int i = 0; i < 10; i++)
	{
		if (i != 9) {
			cout << "|    0" << (i + 1) << "    |     |     |     |     |  || |    0" << (i + 1) << "    |     |     |     |     |\n";
			cout << "|----------|-----|-----|-----|-----|  || |----------|-----|-----|-----|-----|\n";
		}
		else
		{
			cout << "|    " << (i + 1) << "    |     |     |     |     |  || |    " << (i + 1) << "    |     |     |     |     |\n";
		}
	}
	cout << "-----------------------------------------------------------------------------\n";
	_getch();
	return 0;
}
int main()
{
	juego();
}

