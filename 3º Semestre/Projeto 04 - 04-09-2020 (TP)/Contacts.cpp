#include <iostream>
#include <string>
#include <ctime>
#include <vector>
using namespace std;

#define TAM 10

class Data
{
private:
	int dia;
	int mes;
	int ano;
public:
	Data(int dia, int mes, int ano)
	{
		this->dia = dia;
		this->mes = mes;
		this->ano = ano;
	}
	Data()
	{
		time_t t;
		struct tm *data;
		
		time(&t);
		data = localtime(&t);
		
		this->dia = data->tm_mday;
		this->mes = data->tm_mon+1;
		this->ano = (data->tm_year + 1900);
	}
	void setDia(int dia)
	{
		this->dia = dia;
	}
	void setMes(int mes)
	{
		this->mes = mes;
	}
	void setAno(int ano)
	{
		this->ano = ano;
	}
	int getDia()
	{
		return this->dia;
	}
	int getMes()
	{
		return this->mes;
	}
	int getAno()
	{
		return this->ano;
	}
	string getData()
	{
		string dia = (this->dia < 10)? "0" + to_string(this->dia) : to_string(this->dia);
		string mes = (this->mes < 10)? "0" + to_string(this->mes) : to_string(this->mes);
		string ano = to_string(this->ano);				
		
		return dia + "/" + mes + "/" + ano;
	}
	Data* dia_seguinte()
	{
		int diasNoMes[12] = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
		Data *d1 = new Data(this->dia, this->mes, this->ano);

		if (d1->ano % 4 == 0)
		{
			diasNoMes[1]++;
		}

		d1->dia++;
		if (d1->dia > diasNoMes[d1->mes - 1])
		{
			d1->dia = 1;
			if (++d1->mes > 12)
			{
				d1->mes = 1;
				d1->ano++;
			}
		}

		return d1;
	}
};

class Contato
{
	private:
		string email;
		string nome;
		string telefone;
		string dtnasc;
		
	public:
		//construtor
		Contato(string email, string nome, string telefone, string dtnasc)
		{
			this->email = email;
			this->nome = nome;
			this->telefone = telefone;
			this->dtnasc = dtnasc;
		}		
		Contato()
		{
			this->email = "";
			this->nome = "";
			this->telefone = "";
			this->dtnasc = "";
		}
		
		//setters
		void setEmail(string email)
		{
			this->email = email;
		}
		void setNome(string nome)
		{
			this->nome = nome;
		}
		void setTelefone(string telefone)
		{
			this->telefone = telefone;
		}
		void setDtnasc(string dtnasc)
		{
			this->dtnasc = dtnasc;
		}
		
		//getters
		string getEmail()
		{
			return this->email;
		}
		string getNome()
		{
			return this->nome;
		}
		string getTelefone()
		{
			return this->telefone;
		}
		string getDtnasc()
		{
			return this->dtnasc;
		}
		
		int getIdade()
		{
			Data *dtHoje = new Data(6, 9, 2020);
			int anoAtual = dtHoje->getAno();
			int anoNasc = stoi((this->dtnasc).substr(6, 4)); // extraindo ano
			int mesNasc = stoi((this->dtnasc).substr(3, 2)); // extraindo mês
			int diaNasc = stoi((this->dtnasc).substr(0, 2)); // extraindo dia
			
			int idade = anoAtual - anoNasc;			
			if(dtHoje->getMes() < mesNasc) {
				idade--;
			} else if(dtHoje->getMes() == mesNasc) {
				if(dtHoje->getDia() < diaNasc) {
					idade--;
				}
			}
			
			return idade;
		}
};


// Funções
// resumo dos contatos e suas respectivas idades
void resumoContatos(Contato *contatos[])
{
	for(int i=0; i<TAM; i++)
	{
		string nome = contatos[i]->getNome();
		string dtNasc = contatos[i]->getDtnasc();
		string idade = to_string(contatos[i]->getIdade());
		cout << nome << " --->\t" << "DN: " << dtNasc << " - " << idade << " anos" << endl;
	}
}

// cálculo da média de idade dos contatos
int calcMedia(Contato *contatos[])
{
	int contIdade = 0;
	// somando idades dos contatos
	for(int i=0; i<TAM; i++)
	{
		contIdade += contatos[i]->getIdade();
	}
	
	return contIdade / TAM;
}

// retornando array com contatos com idade acima da média
vector<string> getAcimaMedia(Contato *contatos[])
{
	vector<string> acimaMedia;	
	for(int i=0; i<TAM; i++)
	{
		if(contatos[i]->getIdade() > calcMedia(contatos))
		{
			acimaMedia.push_back(contatos[i]->getNome());
		}
	}
	
	return acimaMedia;	
}

// retorna quantidade de contatos maiores de idade
int getQuantMaior18(Contato *contatos[])
{
	int quantMaior18 = 0;
	for(int i=0; i<TAM; i++)
	{
		if(contatos[i]->getIdade() >= 18)
		{
			quantMaior18++;
		}
	}
	
	return quantMaior18;
}

// retorna um array de strings com os nomes dos contatos mais velhos.
vector<string> getMaisVelho(Contato *contatos[])
{
	vector<string> contatoMaior;
	
	int maior = 0;
	for(int i=0; i<TAM; i++)
	{
		if(i == 0)
		{
			maior = contatos[i]->getIdade();
			contatoMaior.push_back(contatos[i]->getNome());
		} else { 
			if(maior < contatos[i]->getIdade())
			{
				maior = contatos[i]->getIdade();
				contatoMaior = {};
				contatoMaior.push_back(contatos[i]->getNome());
			} else if(maior == contatos[i]->getIdade())
			{
				contatoMaior.push_back(contatos[i]->getNome());
			}
		}
	}
	
	contatoMaior.push_back(to_string(maior)); // adicionando idade mais velha no final do array
	return contatoMaior;
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL,"");
	
	Contato *contatos[TAM];
	/*contatos[0] = new Contato("eleson@hotmail.com", "Eleson Souza", "(13)99999-0000", "04/11/1996");
	contatos[1] = new Contato("camila@gmail.com", "Camila Santos", "(13)95268-7825", "05/04/1988");
	contatos[2] = new Contato("isabela@yahoo.com", "Isabella Vitória", "(13)98236-4536", "02/06/2004");
	contatos[3] = new Contato("matheus@gmail.com", "Matheus Santana", "(13)96345-2630", "22/11/1980");
	contatos[4] = new Contato("maiara@hotmail.com", "Maiara Pereira", "(13)98634-2365", "15/07/1989");*/
	//Contato *contatos[3];
	
	string nome, email, telefone, dtNasc;
	for(int i=0; i<TAM; i++)
	{
		cout << "CONTATO " << i + 1 << endl;
		cout << "---------------------------" << endl;
		
		cout << "Nome: ";
		cin >> nome;
		
		cout << "E-mail: ";
		cin >> email;
		
		cout << "Telefone: ";
		cin >> telefone;
		
		cout << "Data Nasc. (dd/mm/aaaa): ";
		cin >> dtNasc;
		
		contatos[i] = new Contato(email, nome, telefone, dtNasc);
		cout << endl;
	}
	
	// EXIBIÇÕES
	// resumo dos contatos e suas idades
	resumoContatos(contatos);
	
	// contatos maiores de idade
	cout << endl << "MÉDIA DAS IDADES: " << calcMedia(contatos) << " anos" << endl;
	cout << endl << "CONTATOS MAIORES DE IDADE: " << getQuantMaior18(contatos) << " CONTATOS" << endl;
	
	// contatos acima da idade geral de idade
	vector<string> nomesAcimaMedia = getAcimaMedia(contatos);
	cout << endl << "CONTATOS ACIMA DA MÉDIA DE IDADE (" << calcMedia(contatos) << " ANOS):" << endl;
	for(int i=0; i<nomesAcimaMedia.size(); i++)
	{
		cout << nomesAcimaMedia[i] << endl;
	}
	
	// contatos mais velhos de idade
	vector<string> nomesMaisVelhos = getMaisVelho(contatos);
	cout << endl << "CONTATOS MAIS VELHOS (" << nomesMaisVelhos[nomesMaisVelhos.size()-1] << " ANOS): " << endl;
	for(int i=0; i<nomesMaisVelhos.size()-1; i++)
	{
		cout << i+1 << " - " << nomesMaisVelhos[i] << endl;
	}

	return 0;
}
