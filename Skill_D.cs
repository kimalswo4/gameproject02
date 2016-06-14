using UnityEngine;
using System.Collections;

public class Skill_D : Skill
{

    public Skill_D()
    {
        minCommandNumber = 5;
        maxCommandNumber = 8;
        commandNumberProbability = new int[] { 10, 50, 30, 10 };
    }

    public override void Action()
    {
        Player.Instance.ActiveSkill(PlayerAttackType.Skill_D);
    }
}
