#include <iostream>
using namespace std;

// função que gera e printa o array em ordem seguindo o "Selection Sort".
void sortArrayBySelection(int length, int *vector) {
	// Ordenando array
	for(int i=0; i<length; i++) {
		for(int x=i+1; x<length; x++) {
			if(vector[i] > vector[x]) {
				int aux = vector[i];
				vector[i] = vector[x];
				vector[x] = aux;
			}
		}
	}
	// Exibindo array ordenado
	for(int i=0; i<length; i++) {
		cout << vector[i] << ((i<length-1)? ", " : "");
	}
	cout << endl;
}

void printArray(int length, int *vector) {
	for(int i=0; i<length; i++) {
		cout << vector[i] << ((i<length-1)? ", " : "");
	}
	cout << endl;
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL, "Portuguese");
	int array[10] = {6,9,1,2,5,4,7,8,3,10};
	int length = sizeof(array)/sizeof(int);
	
	cout << "ORDENAÇÂO POR SELECTION SORT" << endl << endl;
	cout << "Vetor original: ";
	printArray(length, array);
	
	cout << "Vetor ordenado: ";
	sortArrayBySelection(length, array);
	
	return 0;
}