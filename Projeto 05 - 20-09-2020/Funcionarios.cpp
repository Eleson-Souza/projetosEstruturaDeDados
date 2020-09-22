#include <iostream>
#include <unistd.h>
using namespace std;

struct Funcionario
{
	int prontuario;
	string nome;
	double salario;
	struct Funcionario *ant;
};

Funcionario* init()
{
	return NULL;
}
/*
int isEmpty(Lista* lista)
{
	return (lista == NULL);
}*/

Funcionario* insert(Funcionario* func, int prontuario, string nome, double salario)
{
	Funcionario* novo = new Funcionario;
	novo->prontuario = prontuario;
	novo->nome = nome;
	novo->salario = salario;
	novo->ant = func;
	return novo;
}

void print(Funcionario* func)
{
	int cont = 1;
	Funcionario* aux;
	aux = func;
	if(aux != NULL) {
		while (aux != NULL)
		{
			cout << "------------------------------" << endl;
			cout << "Funcioário " << cont << endl;
			cout << "------------------------------" << endl;
			cout << "Nome: " << aux->nome << endl;
			cout << "Prontuario: " << aux->prontuario << endl;
			cout << "Salario: " << aux->salario << endl << endl;
			aux = aux->ant;
			cont++;
		}
	} else {
		cout << endl << "Nenhum funcionário para ser exibido!" << endl << endl;
	}
}

bool validarProntuario(Funcionario* func, int prontuario) 
{
	int contDuplic = 0;
	Funcionario* aux;
	aux = func;
	while(aux != NULL)
	{
		if(aux->prontuario == prontuario) {
			contDuplic++;
		}
		aux = aux->ant;
	}
	
	return (contDuplic == 0)? true : false;
		
}

Funcionario* find(Funcionario* func, int pront)
{
	Funcionario* aux;
	aux = func;
	while (aux != NULL && aux->prontuario != pront)
	{
		aux = aux->ant;
	}
	return aux;
}

Funcionario* remove(Funcionario* func, int pront)
{
	Funcionario *ant = NULL;
	Funcionario *aux;
	
	aux = func;
	while (aux != NULL && aux->prontuario != pront)
	{
		ant = aux;
		aux = aux->ant;
	}
	if (aux == NULL)
	{
		return func;
	}
	if (ant == NULL) // era primeiro
	{
		func = aux->ant;
	}
	else // estava no meio
	{
		ant->ant = aux->ant;
	}
	free(aux);
	return func;
}

void freeList(Funcionario* func)
{
	Funcionario *aux;
	aux = func;
	while (aux != NULL)
	{
		Funcionario *ant = aux->ant;
		free(aux);
		aux = ant;
	}
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL,"Portuguese");	
	int opcao, prontuario;
	double salario;
	string nome;
	
	Funcionario *funcionario;
	funcionario = init();
	
	do {
		cout << "0. Sair" << endl;
		cout << "1. Incluir" << endl;
		cout << "2. Excluir" << endl;
		cout << "3. Pesquisar" << endl;
		cout << "4. Listar" << endl;
		
		cout << "==> ";
		cin >> opcao;
		
		switch(opcao) {
			case 0:
				cout << endl << "Programa encerrado!";
				break;
			case 1: // Inclusão de funcionários
			{
				cout << endl;
				cout << "INCLUSÃO DE FUNCIONÁRIO" << endl;
				cout << "Nome: ";
				cin >> nome;
				cout << "Prontuário: ";
				cin >> prontuario;
				cout << "Salário: R$ ";
				cin >> salario;
				
				bool eValido = validarProntuario(funcionario, prontuario);
				
				if(eValido) {
					funcionario = insert(funcionario, prontuario, nome, salario);
					cout << endl << "Inclusão realizada com sucesso!" << endl << endl;	
				} else {
					cout << endl << "O prontuário inserido já existe, tente novamente com outro!" << endl << endl;
				}
				sleep(1.5);
				break;	
			}
			case 2: // Deleção de uma funcionário
			{
				int prontBusca;
				cout << endl;
				cout << "EXCLUSÃO DE FUNCIONÁRIO" << endl;
				cout << "Informe o número do prontuário: ";
				cin >> prontBusca;
				
				Funcionario* aux;
				aux = remove(funcionario, prontBusca);
				if(funcionario != aux){ // se for diferente, houve mudança na lista
					cout << endl << "Funcionário excluído com sucesso!" << endl << endl;
					funcionario = aux;
				} else {
					cout << endl << "Nenhum funcionário encontrado com esse prontuário!" << endl << endl;
				}
				sleep(1.5);
				break;	
			}
			case 3: // Pesquisa de funcionários pelo prontuário
			{
				int prontBusca;
				cout << endl;
				cout << "PESQUISA POR FUNCIONÁRIO" << endl;
				cout << "Informe o número do prontuário: ";
				cin >> prontBusca;
				
				Funcionario* aux;
				aux = find(funcionario, prontBusca);
				
				cout << endl;
				if(aux != NULL) {					
					cout << "Nome: " << aux->nome << endl;
					cout << "Prontuário: " << aux->prontuario << endl;
					cout << "Salário: " << aux->salario << endl;	
				} else {
					cout << "Nenhum funcionário encontrado com esse prontuário!";
				}				
				cout << endl << endl;
				sleep(1.5);
				break;
			}
			case 4: // Listar funcionários
				cout << endl << "LISTAGEM DE FUNCIOÁRIOS" << endl;
				print(funcionario);
				sleep(1.5);
				break;
			default:
				cout << "Opção inválida, tente novamente!" << endl << endl;
				sleep(1.5);
				break;
		}
	} while(opcao != 0);
	
	freeList(funcionario);
	
	return 0;
}
