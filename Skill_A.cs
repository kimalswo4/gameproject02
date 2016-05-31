using UnityEngine;
using System.Collections;

public class Skill_A : Skill {
    public Skill_A() : base(3,6)
    {
        AddCommandProbability(3, 20);
        AddCommandProbability(4, 40);
        AddCommandProbability(5, 30);
        AddCommandProbability(6, 10);
        SetPercent();
    }
    
}
