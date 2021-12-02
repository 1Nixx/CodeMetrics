using System;
using System.Collections.Generic;
using System.Text;

class a { 
    int[] bubbleSort(int[] arr, int arrSize)
    {
        int tmp = 0;
        for (int i = 0; i < arrSize; i++)
        {
            for (int j = 0; j < arrSize - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    tmp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                }
            }
        }
        return arr;
    }

    int IsExprTrue(dynamic num1 , dynamic num2)
    {
        if (num1 >= 100 && num2 == 4)
            return num1 << num2;
        else if (num1 == 10 || num2 == 4)
            return (num1 + 30) >> num2;
        else
            return num1 & num2;
    }

    void main()
	{
        string amount = Console.ReadLine();
        int n = int.Parse(amount);
		Console.WriteLine(n);
        int[] nums = { 9, 8, 7, 1, 4};
		Console.WriteLine(nums);

        int unused = 25 / n + 36 * n;

        int[] arrForBubble = { 2, 34, 46, 23, 56, 1, 3, 5, 67, 5 };
        bubbleSort(arrForBubble, arrForBubble.Length);
        Console.WriteLine("Bubble sort: ", arrForBubble);

        // Add Input
        string number1 = Console.ReadLine();
        string number2 = Console.ReadLine();

        int exprRes = IsExprTrue(number1, number2);
        Console.WriteLine(exprRes);
	}
    }