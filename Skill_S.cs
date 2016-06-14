using UnityEngine;
using System.Collections;

public class Skill_S : Skill
{

    public Skill_S()
    {
        minCommandNumber = 4;
        maxCommandNumber = 7;
        commandNumberProbability = new int[] { 20, 40, 30, 10 };
    }

    public override void Action()
    {
        Player.Instance.ActiveSkill(PlayerAttackType.Skill_S);
    }
}
