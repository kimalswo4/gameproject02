using UnityEngine;
using System.Collections;

public class Skill_F : Skill
{

    public Skill_F()
    {
        minCommandNumber = 6;
        maxCommandNumber = 8;
        commandNumberProbability = new int[] { 20, 30, 50};
    }

    public override void Action()
    {
        Player.Instance.ActiveSkill(PlayerAttackType.Skill_F);
    }
}
