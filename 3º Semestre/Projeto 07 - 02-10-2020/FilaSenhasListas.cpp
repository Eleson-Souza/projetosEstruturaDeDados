#include <iostream>
#include <unistd.h>
using namespace std;

// struct nó/valores das senhas
struct Senha {
	int numSenha;
	struct Senha *prox;
};

// Fila de senhas geradas
struct SenhasGeradas {
	Senha *ini;
	Senha *fim;
};

// Fila de senhas atendidas
struct SenhasAtendidas {
	Senha *ini;
	Senha *fim;
};

// inicialização e retorno da filha SenhasGeradas
SenhasGeradas* initSenhasGeradas() {
	SenhasGeradas *sg = new SenhasGeradas;
	sg->ini = NULL;
	sg->fim = NULL;
	return sg;
};

// inicialização e retorno da filha SenhasAtendidas
SenhasAtendidas* initSenhasAtendidas() {
	SenhasAtendidas *sa = new SenhasAtendidas;
	sa->ini = NULL;
	sa->fim = NULL;
	return sa;
};

// verificando se a fila senhas geradas está vazia, retornando 0 ou 1 (true ou false).
int isEmptyGeradas(SenhasGeradas *sg) {
	return (sg->ini == NULL);
};

// verificando se a fila senhas atendidas está vazia, retornando 0 ou 1 (true ou false).
int isEmptyAtendidas(SenhasAtendidas *sa) {
	return (sa->ini == NULL);
};

// enfileirando senhas na fila SenhasGeradas.
void enqueueGeradas(SenhasGeradas *sg, int valor) {
	Senha *senha = new Senha;
	senha->numSenha = valor;
	senha->prox = NULL;
	if(isEmptyGeradas(sg)) {
		sg->ini = senha;
	} else {
		sg->fim->prox = senha;
	}
	sg->fim = senha;
};

// desenfileirando senhas na fila SenhasGeradas.
void dequeueGeradas(SenhasGeradas *sg) {
	Senha *senha = sg->ini;
	sg->ini = senha->prox;
	if(sg->ini == NULL) {
		sg->fim = NULL;
	}
	free(senha);
};

// inserindo senhas na fila SenhasAtendidas.
void enqueueAtendidas(SenhasAtendidas *sa, SenhasGeradas *sg) {
	Senha *senha = new Senha;
	senha->numSenha = sg->ini->numSenha;
	senha->prox = NULL;
	if(isEmptyAtendidas(sa)) {
		sa->ini = senha;
	} else {
		sa->fim->prox = senha;
	}
	sa->fim = senha;
};

// exibindo dados da fila de senhas atendidas
void printAtendidas(SenhasAtendidas *sa) {
	Senha *senha;
	senha = sa->ini;
	
	cout << endl;
	cout << "Fila de senhas atendidas" << endl;
	while(senha != NULL) {
		cout << senha->numSenha << " | ";
		senha = senha->prox;
	}
};

// realizando e retornando contagem de senhas atendidas
int countAtendidas(SenhasAtendidas *sa) {
	int qtde = 0;
	Senha *senha;
	senha = sa->ini;
	while(senha != NULL) {
		qtde++;
		senha = senha->prox;
	}
	return qtde;
}

// realizando e retornando contagem de senhas geradas
int countGeradas(SenhasGeradas *sg) {
	int qtde = 0;
	Senha *senha;
	senha = sg->ini;
	while(senha != NULL) {
		qtde++;
		senha = senha->prox;
	}
	return qtde;
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL,"Portuguese");
	
	SenhasGeradas *senhaGer;
	senhaGer = initSenhasGeradas();
	
	SenhasAtendidas *senhaAtend;
	senhaAtend = initSenhasAtendidas();
	
	int opcao, senhaGerada = 0;
	do {		
		cout << endl;
		cout << "0. Sair" << endl;
		cout << "1. Gerar senha" << endl;
		cout << "2. Realizar atendimento" << endl;
		
		cout << endl;
		cout << "Senhas esperando atendimento  : " << countGeradas(senhaGer) << endl;
		cout << "Senhas atendidas até o momento: " << countAtendidas(senhaAtend) << endl;
		
		cout << endl << "Escolha -> ";
		cin >> opcao;
		
		switch(opcao)
		{
			// Sair
			case 0: 
			{				
				int isEmpty = isEmptyGeradas(senhaGer);
				// se a fila senhas geradas não estiverem vazias, bloqueia a saída do programa.
				if(isEmpty == 0) {
					cout << "ATENÇÃO, VOCÊ NÃO PODE FINALIZAR, POIS AINDA EXISTEM SENHAS A SEREM ATENDIDAS!" << endl;
					opcao = -1;
					sleep(1.5);
				}
				break;
			}
			// Gerar senha
			case 1:
				// enfileirando na SenhasGeradas.
				enqueueGeradas(senhaGer, ++senhaGerada);
				break;
			// Realizar atendimento
			case 2: 
				// se não existirem senhas geradas, não existe atendimento.
				if(isEmptyGeradas(senhaGer)) {
					cout << "NÃO EXISTEM SENHAS PARA SEREM ATENDIDAS!" << endl;
					sleep(1.5);
				} else { // enfileirando em senhas atendidas e desenfileirando em senhas geradas.
					enqueueAtendidas(senhaAtend, senhaGer);
					dequeueGeradas(senhaGer);
				}
				break;
		}
				
	} while(opcao != 0);
	
	printAtendidas(senhaAtend);
	
	cout << endl << endl;
	cout << "Total de senhas atendidas: " << countAtendidas(senhaAtend) << endl;
	
	return 0;
};