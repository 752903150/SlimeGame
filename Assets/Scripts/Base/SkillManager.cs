using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Skill {
    DoubleJump,
    SwordNormalAttack,
    SwordSprintAttack,
    SwordPickOut,
}

public class SkillManager
{
    bool[] learnSkill;
    public SkillManager()
    {
        learnSkill = new bool[4];
        int i = 0;
        for(;i<4;++i)
        {
            learnSkill[i] = false;
        }
    }
    public void Learn(Skill skill)
    {
        learnSkill[(int)skill] = true;
    }
    public bool Islearn(Skill skill)
    {
        return learnSkill[(int)skill];
    }
}
