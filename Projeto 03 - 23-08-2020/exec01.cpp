#include <iostream>
using namespace std;

int calcularMedia(int array[])
{
	int count = 0;
	
	for(int i = 0; i < 10; i++)
	{
		count += *array; // *array = conte�do do endere�o
		array++; // incrementando para o pr�ximo endere�o do array
	}
	
	return count / 10;
}

int calcNumsAcimaMedia(int array[], double media) 
{
	int quantAcimaMedia = 0;
	
	for(int i = 0; i < 10; i++)
	{
		if(*array > media)
		{
			quantAcimaMedia++;
		}
		array++;
	}
	
	return quantAcimaMedia;
}

int main(int argc, char** argv)
{
	setlocale(LC_ALL,"");
	
	int nums[10] = {};
	
	for(int i = 0; i < 10; i++) 
	{
		cout << "Digite o " << i + 1 << "� n�mero: ";
		cin >> nums[i];
	}
	
	double media = calcularMedia(nums);
	int quantNums = calcNumsAcimaMedia(nums, media);
	
	cout << endl;
	cout << "Quantidade de valores acima da m�dia (" << media << "): " << quantNums;
	 
	return 0;
}