using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttributes : CharacterAttributes
{
    public float toward=1f;//移动朝向
    public float moveRate=1f;//移动倍率
    public MonsterAttributes(int hp = 5, int health = 5, float speed = 2f, int id = 0,float toward = 1f,float moveRate = 1f):base(hp, health, speed, id)
    {
        this.toward = toward;
        this.moveRate = moveRate;
    }
}
