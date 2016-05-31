using UnityEngine;
using System.Collections;

public class Skill{

    protected readonly int CommandRangeMax;
    protected readonly int CommandRangeMin;
    protected int CurrentCommandNumber;
    protected int Damage;
    protected int CoolTime;
    protected Animation SkillAnimation;
    protected int[] CommandRangeProbability;
    protected int[] CommandPercent;

    public Skill(int min, int max)
    {
        CommandRangeMin = min;
        CommandRangeMax = max;
        CommandRangeProbability = new int[CommandRangeMax - CommandRangeMin + 1];
        CommandPercent = new int[100];
    }

    public void AddCommandProbability(int number, int probability)
    {
        if (number < CommandRangeMin) 
        { 
            Debug.Log("에러:스킬스크립트AddCommandProbability");
            return;
        }
        CommandRangeProbability[number - CommandRangeMin] = probability;
    }

    public void SetPercent()
    {
        int CheckPercent = -1;
        for (int index = 0; index < CommandRangeProbability.Length; index++)
        {
            for (int num = CheckPercent + 1; num < CommandRangeProbability[index]; num++)
            {
                CommandPercent[num] = CommandRangeProbability[index];
                CheckPercent = num;
            }
        }
    }

    public void SetCommandNumber()
    {
        CurrentCommandNumber = CommandPercent[Random.Range(0, 100)];
        
    }
}
