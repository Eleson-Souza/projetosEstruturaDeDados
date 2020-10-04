#include <iostream>
using  namespace std;

struct No {
	int valor;
	struct No *prox;
};

struct Par {
	No *topo;
};

struct Impar {
	No *topo;
};

Par* initPar() {
	Par *p = new Par;
	p->topo = NULL;
	return p;
}

Impar* initImpar() {
	Impar *i = new Impar;
	i->topo = NULL;
	return i;
}

void pushPar(Par *p, int v) {
	No *no = new No;
	no->valor = v;
	no->prox = p->topo;
	p->topo = no;
}

void pushImpar(Impar *i, int v) {
	No *no = new No;
	no->valor = v;
	no->prox = i->topo;
	i->topo = no;
}

void printPares(Par *p) {
	No *no;
	no = p->topo;
	
	cout << endl << "Pilha de Pares" << endl;
	while(no != NULL) {
		cout << no->valor << endl;
		no = no->prox;
	}
	cout << "------------------------" << endl;
}

void printImpares(Impar *i) {
	No *no;
	no = i->topo;
	
	cout << endl << "Pilha de Ímpares" << endl;
	while(no != NULL) {
		cout << no->valor << endl;
		no = no->prox;
	}
	cout << "------------------------" << endl;
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