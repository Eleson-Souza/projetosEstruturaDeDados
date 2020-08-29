#include <iostream>
#include <stdlib.h>
#include <stdio.h>
using namespace std;

#define LIN 3
#define COL 3

// verificando igualdade entre matrizes	
string verificarMatrizes(int *mat1[], int *mat2[])
{
	for(int l = 0; l < LIN; l++)
	{
		for(int c = 0; c < COL; c++)
		{
			if(mat1[l][c] != mat2[l][c])
			{
				return "NÃO SÃO IGUAIS!";
				break;
			}
		}
	}
	
	return "SÃO IGUAIS!";
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL,"");
	
	/*----------- Matriz 1 -------------------------------------------------*/
	int l;
	int c;
	
	int **mat1;
	mat1 = (int**)malloc(sizeof(int) * LIN); // alocando memória no vetor de ponteiros
	
	for(int i = 0; i < 5; i++)
	{
		mat1[i] = (int*)malloc(sizeof(int) * COL); // alocando memoria para cada elemento
	}
	
	cout << "PRIMEIRA MATRIZ" << endl;
	// populando matriz 1
	for(l = 0; l < LIN; l++)
	{
		for(c = 0; c < COL; c++)
		{
			cout << "Informe o valor (" << l << "," << c << "): ";
			cin >> mat1[l][c];
		}
	}
	
	// exibindo matriz 1
	for(l = 0; l < LIN; l++)
	{
		for(c = 0; c < COL; c++)
		{
			cout << (c == 0?" | ":"") << mat1[l][c] << " | ";
		}
		cout << endl;
	}
	
	/*----------- Matriz 2 -------------------------------------------------*/
	int x;
	int y;
	
	int **mat2;
	mat2 = (int**)malloc(sizeof(int) * LIN);
	
	for(int i = 0; i < LIN; i++)
	{
		mat2[i] = (int*)malloc(sizeof(int) * COL);
	}
	
	cout << endl << "SEGUNDA MATRIZ" << endl;
	// populando matriz 2
	for(x = 0; x < LIN; x++)
	{
		for(y = 0; y < COL; y++)
		{
			cout << "Informe o valor (" << x << "," << y << "): ";
			cin >> mat2[x][y];
		}
	}
	
	// exibindo matriz 2
	for(x = 0; x < LIN; x++)
	{
		for(y = 0; y < COL; y++)
		{
			cout << (y == 0?" | ":"") << mat2[x][y] << " | ";
		}
		cout << endl;
	}
	
	
	// verificando igualdade entre matrizes	
	string result = verificarMatrizes(mat1, mat2);
	cout << endl << "As matrizes são iguais? " << result;
	
	return 0;
}