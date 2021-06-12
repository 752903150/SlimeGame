using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack// : AttackInterface
{
    /*int monsterLayer = 1 << 10;
    Rigidbody2D rigidbody;
    Transform transform;
    Vector3 attackVector;
    Monster monster;
    public void InitObject(GameObject gameObject, RoleAttribute roleAttributes)//初始化对象
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        transform = gameObject.transform;
        attackVector = new Vector3(0f, 0f, 0f);

    }
    public void RoleNormalAttack(AttackModel attackModel, bool forward)//人物普通攻击接口实现
    {
        if (attackModel == AttackModel.Normal)
        {
            NormalAttack(forward);
        }
    }
    void NormalAttack(bool forward)
    {
        if (toward > 0 && Physics2D.Raycast(transform.position, checkVectors[0], 4f, playerLayer) ||
          toward < 0 && Physics2D.Raycast(transform.position, checkVectors[1], 4f, playerLayer))
        {
            moveRate += 0.01F;
        }
        else
        {
            moveRate = 1f;
        }
    }
    void AttackHits(GameObject gameObject)//攻击命中
    {
        Debug.Log("攻击命中");
        monster = gameObject.GetComponent<Monster>();
        monster.Injured(attackVector.x, 1f);
    }*/
}
