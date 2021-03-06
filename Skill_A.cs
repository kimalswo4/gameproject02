﻿using UnityEngine;
using System.Collections;

public class Skill_A : Skill
{

    public Skill_A()
    {
        minCommandNumber = 3;
        maxCommandNumber = 6;
        commandNumberProbability = new int[] { 20, 40, 30, 10 };
    }

    public override void Action()
    {
        Player.Instance.ActiveSkill(PlayerAttackType.Skill_A);
    }
}
