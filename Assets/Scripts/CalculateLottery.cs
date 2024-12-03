using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalculateLottery : MonoBehaviour
{
    [SerializeField] TMP_InputField[] numeros = new TMP_InputField[5];
    [SerializeField] TMP_InputField reintegro;
    int[] numbers = new int[5];
    int specialNumber = 0;
    int numbresOfAttemps = 0;

    public void Calculate()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = Int32.Parse(numeros[i].text);
        }
        specialNumber = Int32.Parse(reintegro.text);
        numbresOfAttemps = 0;
        StartCoroutine(Calculating());
    }

    IEnumerator Calculating()
    {
        bool condition = false;
        while (condition == false)
        {
            LotteryNumber[] numbersOfLottery = new LotteryNumber[5];
            int specialnumberAux = 0;

            for (int i = 0; i < numbersOfLottery.Length; i++)
                numbersOfLottery[i] = new LotteryNumber(0, false);

            for (int i = 0; i < numbersOfLottery.Length; i++)
            {
                numbersOfLottery[i].number = UnityEngine.Random.Range(1, 55);
                for (int j = 0; j < numbersOfLottery.Length; j++)
                {
                    if(j != i)
                    {
                        if(numbersOfLottery[i].number == numbersOfLottery[j].number)
                        {
                            i--;
                            break;
                        }
                    }
                }
            }

            Debug.Log("Numeros que salieron: "+ numbersOfLottery[0].number + " " + numbersOfLottery[1].number + " " + numbersOfLottery[2].number + " " + numbersOfLottery[3].number + " " + numbersOfLottery[4].number);
            specialnumberAux = UnityEngine.Random.Range(0, 10);
            Debug.Log("Reintegro " + specialnumberAux);

            bool conditionAux = true;

            for (int i = 0; i < numbersOfLottery.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    numbersOfLottery[i].appear = numbersOfLottery[i].number == numbers[j];
                    if (numbersOfLottery[i].appear)
                        break;
                }
            }
            for (int i = 0; i < numbersOfLottery.Length; i++)
            {
                conditionAux = conditionAux && numbersOfLottery[i].appear;
            }
            //conditionAux = conditionAux && numbersOfLottery[i] == numbers[i];
            conditionAux = conditionAux && specialnumberAux == specialNumber;

            if (conditionAux)
                condition = true;

            numbresOfAttemps++;
            Debug.Log(numbresOfAttemps);
            yield return null;
        }

        Debug.Log("Congratulations!! You win the lottery at only " + numbresOfAttemps + " attemps!");
    }

    class LotteryNumber
    {
        public int number;
        public bool appear;

        public LotteryNumber (int number, bool appear)
        {
            this.number = number;
            this.appear = appear;
        }
    }

}
