#include <stdio.h>
#include <stdlib.h>

int main()
{
	int opcao = 1;
	int carrinho, tipoPipoca, i, quantPipocas, elemento;
	int *faturamento;
	
	faturamento = (int*)malloc(15 * 2 * sizeof(int));
	
	for(i = 0; i < 30; i++)
	{
		*(faturamento + i) = 0;
	}
	
	while(opcao != 0)
	{
		printf("\n0. Fim\n");
		printf("1. Vender pipoca\n");
		printf("2. Mostrar faturamento\n");
		scanf("%i", &opcao);
		
		if(opcao == 1)
		{
			printf("\nInforme em qual carrinho a compra sera realizada (1 - 15): ");
			scanf("%i", &carrinho);
			
			printf("\nQual o tipo de pipoca ira comprar? \n");
			printf("\t1. Doce:    R$ 7,50\n");
			printf("\t2. Salgada: R$ 5,00\n");
			printf("=>");
			scanf("%i", &tipoPipoca);	
			printf("\n\nQuantas pipocas? ");
			scanf("%i", &quantPipocas);

			if(tipoPipoca == 1)
			{
				elemento = 2 * (carrinho - 1) + 0;
				*(faturamento + elemento) += quantPipocas;			   	
			} 
			else if(tipoPipoca == 2)
			{
				elemento = 2 * (carrinho - 1) + 1;
				*(faturamento + elemento) += quantPipocas;
			}
		}
		else if(opcao == 2)
		{
			// ...
			int l, c, aux = 0;
			double salg, doc;
			for(l = 0; l < 15; l++)
			{
				printf("\t");
				
				doc = *(faturamento + (2 * l + 0)) * 7.5;
				salg = *(faturamento + (2 * l + 1)) * 5;
				printf("%io carrinho: %i doces \t %i salgadas \t Total: R$ %.2f", 
				l + 1,
				*(faturamento + (2 * l + 0)),
				*(faturamento + (2 * l + 1)),
				salg + doc);
				aux++;
				
				printf("\n");
			}
			/*double total = 0, salg, doc;
			for(l = 0; l < 15; l++)
			{
				doc = faturamento[l][0] * 7.5;
				salg = faturamento[l][1] * 5.0;
   			    printf("%i %i %f %i %f", 
				   (l+1), 
				   faturamento[l][0],
				   doc,
   				   faturamento[l][1],
                   salg);

				printf("\n");
			}
		}
		else if (opcao == 0)
		{
			break;
		}*/
	    }	
	}
}