#include <stdio.h>
#include <stdlib.h>

void arraySort(int tempoCarros[], int numeroCarros[], int quantCarros) 
{
	int aux1, aux2, i, x;
	for(i = 0; i < quantCarros; i++) {
	    for(x = i+1; x < quantCarros; x++) {
	        if(tempoCarros[i] > tempoCarros[x]) {
	        	// Ordena array de tempo
	            aux1 = tempoCarros[i];
	            tempoCarros[i] = tempoCarros[x];
	            tempoCarros[x] = aux1;
	            
	            // Ordena o array de numero do carro de acordo com o tempo
	            aux2 = numeroCarros[i];
	            numeroCarros[i] = numeroCarros[x];
	            numeroCarros[x] = aux2;
	        }
	    }
	}
	
	printf("\nGrid de Largada: \n");
	for (i = 0; i < quantCarros; i++ ) {
      printf("%do colocado: carro %d = %d segundos\n", i+1, numeroCarros[i], tempoCarros[i]);
   }
}

int main()
{
	int quantCarros;
	int tempo;
	printf("Informe a quantidade de carros da corrida: ");
	scanf("%i", &quantCarros);
	
	int *tempoCarros;
	int *numCarros;
	
	tempoCarros = (int*)malloc(quantCarros*sizeof(int));
	numCarros = (int*)malloc(quantCarros*sizeof(int));
	
	int i;
	for (i = 0; i < quantCarros; i++)
	{
		printf("Tempo da volta (segundos) do carro %i: ", i + 1);
		scanf("%i", &tempo);
		tempoCarros[i] = tempo;
		numCarros[i] = i + 1;
	}
	
	arraySort(tempoCarros, numCarros, quantCarros);

	return 0;
}