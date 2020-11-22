#include <iostream>
using  namespace std;

#define MAX 30

struct Par {
	int qtde;
	int nos[MAX];
};

struct Impar {
	int qtde;
	int nos[MAX];
};

Par* initPar() {
	Par *p = new Par;
	p->qtde = 0;
	return p;
}

Impar* initImpar() {
	Impar *i = new Impar;
	i->qtde = 0;
	return i;
}

void pushPar(Par *p, int v) {
	p->nos[p->qtde] = v;
	p->qtde++;
}

void pushImpar(Impar *i, int v) {
	i->nos[i->qtde] = v;
	i->qtde++;
}

void printPares(Par *p) {
	cout << endl << "Pilha de Pares" << endl;
	for(int i = p->qtde-1; i>=0; i--) {
		cout << p->nos[i] << endl;
	}
	cout << "------------------" << endl;
}

void printImpares(Impar *i) {
	cout << endl << "Pilha de Ímpares" << endl;
	for(int x = i->qtde-1; x>=0; x--) {
		cout << i->nos[x] << endl;
	}
	cout << "------------------" << endl;
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL,"Portuguese");
	
	Par *parInt;
	parInt = initPar();
	
	Impar *imparInt;
	imparInt = initImpar();
	
	int num, ant;
	for(int i=0; i<30; i++) {
		cout << "Digite o número " << i+1 << ": ";
		cin >> num;
		
		while(i != 0 && num <= ant) {
			cout << "O número tem que ser maior que o anterior (" << ant << ")!" << endl;
			cout << "Digite o número " << i+1 << ": ";
			cin >> num;
		}
		
		if(num % 2 == 0) {
			pushPar(parInt, num);
		} else {
			pushImpar(imparInt, num);
		}
		ant = num;
	}
	
	printPares(parInt);
	printImpares(imparInt);
	
	return 0;
}