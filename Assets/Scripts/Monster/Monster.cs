using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    int groundLayer = 1 << 8;
    int playerLayer = 1 << 9;
    int monsterLayer = 1 << 10;
    Vector3[] checkVectors;
    Vector3 moveVector;
    float toward;//移动朝向
    float moveRate;//移动倍率
    new Rigidbody2D rigidbody2D;
    bool isInjured = false;
    bool isFly = false;
    InjuredInterface injuredInterface;
    public void RandomMove()//随机移动
    {
        if(Turning())
        {
            toward *= -1;
        }
        if(!isInjured)
        {
            moveVector.x = toward * moveRate;

            rigidbody2D.velocity = moveVector;
            Attack();
        }
        else if(isFly && Physics2D.Raycast(transform.position + checkVectors[3], checkVectors[2], 0.6f))
        {
            isInjured = false;
            isFly = false;
        }
    }
    public void SetMonster(Rigidbody2D rigidbody2D)
    {
        this.rigidbody2D = rigidbody2D;
        injuredInterface = new RoleInjured();
        injuredInterface.SetPlayer(this.rigidbody2D);
    }
    bool Turning()//判断是否需要转弯
    {
        //Debug.Log(Physics2D.Raycast(transform.position, checkVectors[0], 1.5f, monsterLayer).collider == null);
        if(!isInjured)
            if(toward > 0 && (Physics2D.Raycast(transform.position, checkVectors[0], 1.5f , groundLayer)||
                Physics2D.Raycast(transform.position + checkVectors[3], checkVectors[0] , 1.5f, monsterLayer)) ||
               toward < 0 && (Physics2D.Raycast(transform.position, checkVectors[1], 1.5f , groundLayer)||
                Physics2D.Raycast(transform.position - checkVectors[3], checkVectors[1] , 1.5f, monsterLayer)) ||
               (toward>0 &&!Physics2D.Raycast(transform.position + checkVectors[3], checkVectors[2], 0.6f)) ||
               (toward<0 && !Physics2D.Raycast(transform.position - checkVectors[3], checkVectors[2], 0.6f)))//四根线判断
                return true;//需要转弯
        return false;//不需要转弯
    }
    void Attack()
    {
        if (toward>0 && Physics2D.Raycast(transform.position, checkVectors[0], 4f, playerLayer) ||
          toward < 0 && Physics2D.Raycast(transform.position, checkVectors[1], 4f, playerLayer))
        {
            moveRate += 0.01F;
        }
        else
        {
            moveRate = 1f;
        }
    }
    public void InitMonster()
    {
        checkVectors = new Vector3[4];
        checkVectors[0] = new Vector3(1f, 0f, 0f);
        checkVectors[1] = new Vector3(-1f, 0f, 0f);
        checkVectors[2] = new Vector3(0f, -1f, 0f);
        checkVectors[3] = new Vector3(1f, 0f, 0f);
        toward = 1f;
        moveVector.y = 0f;
        moveVector.z = 0f;
        moveRate = 1f;
    }
    public void Injured(float forward,float stopTime , InjuredModel injuredModel)
    {
        isInjured = true;
        moveRate = 1f;
        if (forward >0)
            injuredInterface.NormalInjured(InjuredModel.RightInjured, injuredModel);
        else
            injuredInterface.NormalInjured(InjuredModel.LeftInjured, injuredModel);
        StartCoroutine(StopMove(stopTime));
    }

    IEnumerator StopMove(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
        isFly = true;
        //isInjured = false;
    }
}
