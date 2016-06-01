using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill{

    protected readonly int CommandRangeMax;
    protected readonly int CommandRangeMin;
    protected int CurrentCommandNumber;
    protected int Damage;
    protected int CoolTime;
    protected Animation SkillAnimation;
    protected int[] CommandRangeProbability;
    protected int[] CommandPercent;
    protected int[] CommandNumber;//랜덤으로 나온 커맨드 총갯수

    protected Queue<KeyCode> Command; 

    public Skill(int min, int max)
    {
        CommandRangeMin = min;
        CommandRangeMax = max;
        CommandRangeProbability = new int[CommandRangeMax - CommandRangeMin + 1];
        CommandPercent = new int[100];
        Command = new Queue<KeyCode>();
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
        int CheckPercent = 0;
        for (int index = 0; index < CommandRangeProbability.Length; index++)
        {
            for (int num = 0; num < CommandRangeProbability[index]; num++)
            {
                CommandPercent[num + CheckPercent] = index + CommandRangeMin;
            }
            CheckPercent = CommandRangeProbability[index];
        }
    }

    public void SetCommandNumber()
    {
        CurrentCommandNumber = CommandPercent[Random.Range(0, 100)]; //0~100퍼센트애들중 커맨드숫자를뽑음
        CommandNumber = new int[CurrentCommandNumber]; //CommandNumber 
        /*for(int index = 0; index < 100; index++)
        {
            Debug.Log(CommandPercent[index]);
        }*/
    }

    public void SetCommand()
    {
        for(int index = 0; index < CurrentCommandNumber; index++)
        {
            switch(Random.Range(0, 4))
            {
                case 0:
                    Command.Enqueue(KeyCode.UpArrow);
                    break;
                case 1:
                    Command.Enqueue(KeyCode.DownArrow);
                    break;
                case 2:
                    Command.Enqueue(KeyCode.RightArrow);
                    break;
                case 3:
                    Command.Enqueue(KeyCode.LeftArrow);
                    break;

            }
        }
    }

    public KeyCode GetCommand()
    {
        //KeyCode key = Command.Dequeue();
        Debug.Log(Command.Dequeue());
        return KeyCode.UpArrow;
    }

    public Queue<KeyCode> GetQueue()
    {
        return Command;
    }
}
