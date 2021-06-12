using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonsterMoveInterface
{
    Vector3[] checkVectors;
    Vector3 moveVector;
    // Start is called before the first frame update
    public void InitMonster()
    {
        
        
        moveVector.y = 0f;
        moveVector.z = 0f;

        monsterAttributes.moveRate = 1f;
        monsterAttributes.toward = 1f;
    }
    public override void InitObject(GameObject gameObject, MonsterAttributes monsterAttributes)
    {
        this.rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this.monsterAttributes = monsterAttributes;
        
    }
    public override void RoleNormalMove(MoveModel model)
    {
        if(model == MoveModel.Run)
        {
            MonsterRun();
        }
        else if(model == MoveModel.Walk)
        {
            MonsterWalk();
        }
        else if(model == MoveModel.Stop)
        {
            MonsterStop();
        }
    }

    public override void Turnning()
    {
        monsterAttributes.toward *= -1;
    }
    void MonsterStop()
    {
        if(monsterAttributes.moveRate != 1F)
            monsterAttributes.moveRate = 1F;
    }
    void MonsterRun()
    {
        monsterAttributes.moveRate += 0.01F;
    }

    void MonsterWalk()
    {
        moveVector.x = monsterAttributes.toward * monsterAttributes.moveRate;

        rigidbody2D.velocity = moveVector;
    }
}
