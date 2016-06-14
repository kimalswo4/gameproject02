using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill {

    protected int atk;
    protected int minCommandNumber;
    protected int maxCommandNumber;
    protected int[] commandNumberProbability;

    private int currentCommandNumber;
    private Queue<KeyCode> commandList;

    public Skill()
    {
        commandList = new Queue<KeyCode>();
    }

    public abstract void Action();  // 스킬 내용
    public int SetCommandNumber()  // 스킬 갯수 랜덤값 설정
    {
        int currentProbability = 0;
        int value = 0;
        int prevValue = 100;
        int randomValue = Random.Range(0, 100);

        for (int index = 0; index <= maxCommandNumber - minCommandNumber; index++)
        {
            value = randomValue - currentProbability;

            if (value > 0 && value < prevValue)
            {
                currentCommandNumber = minCommandNumber + index;
                prevValue = value;
            }

            currentProbability += commandNumberProbability[index];
        }

        return currentCommandNumber;
    }
    public void SetCommand()    // 커맨드 랜덤 생성
    {
        commandList.Clear();
        int[] command = new int[currentCommandNumber];
        for (int index = 0; index < currentCommandNumber; index++)
        {
            command[index] = Random.Range(0, 4);
            switch (command[index])
            {
                case 0: // UpArrow
                    commandList.Enqueue(KeyCode.UpArrow);
                    break;
                case 1: // DownArrow
                    commandList.Enqueue(KeyCode.DownArrow);
                    break;
                case 2: // LeftArrow
                    commandList.Enqueue(KeyCode.LeftArrow);
                    break;
                case 3: // RightArrow
                    commandList.Enqueue(KeyCode.RightArrow);
                    break;
            }
        }
        UIManager.Instance.SetSkillCommand(command);    // 이미지 생성
    }

    public CommandResult EqualCommand(KeyCode key)
    {
        if (commandList.Dequeue() == key)
        {
            if (commandList.Count <= 0)
                return CommandResult.End;  // 끝
            else
                return CommandResult.Success;   // 성공
        }
        else
            return CommandResult.Fail;       // 실패
    }
}
