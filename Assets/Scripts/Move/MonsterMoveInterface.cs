using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMoveInterface : MoveInterface
{
    public Rigidbody2D rigidbody2D;
    public MonsterAttributes monsterAttributes;

    public abstract void InitObject(GameObject gameObject, MonsterAttributes monsterAttributes);

    public abstract void Turnning();

}
