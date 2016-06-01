using UnityEngine;
using System.Collections;

public class Skill_B : Skill {
    public Skill_B() : base(3,6)
    {
        AddCommandProbability(3, 20);
        AddCommandProbability(4, 40);
        AddCommandProbability(5, 30);
        AddCommandProbability(6, 10);
        SetPercent();
    }
}
