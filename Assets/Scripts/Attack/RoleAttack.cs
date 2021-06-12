using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAttack : AttackInterface
{
    int monsterLayer = 1 << 10;
    Rigidbody2D rigidbody;
    Transform transform;
    Vector3 attackVector;
    Enemy enemy;
    public void InitObject(GameObject gameObject , RoleAttribute roleAttributes)//初始化对象
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        transform = gameObject.transform;
        attackVector = new Vector3(0f, 0f, 0f);

    }
    public void RoleNormalAttack(AttackModel attackModel, bool forward)//人物普通攻击接口实现
    {
        if(attackModel == AttackModel.Normal)
        {
            NormalAttack(forward);
        }
    }
    void NormalAttack(bool forward)
    {
        if(forward)
        {
            attackVector.x = 1f;
        }
        else
        {
            attackVector.x = -1f;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackVector, 0.6f, monsterLayer);
        if (hit.collider != null)
        {
            AttackHits(hit.collider.gameObject);
        }
    }
    void AttackHits(GameObject gameObject)//攻击命中
    {
        if (gameObject.GetComponent<NormalSlime>() != null)
            enemy = gameObject.GetComponent<NormalSlime>().enemy;
        else
            enemy = null;
        if(enemy!= null)
        {
            enemy.Injured(transform.localPosition.x < gameObject.transform.localPosition.x, InjuredModel.Normal);
            gameObject.GetComponent<NormalSlime>().InjureStop();
        }
        
    }
}
