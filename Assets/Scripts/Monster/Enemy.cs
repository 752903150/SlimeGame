using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RoleBase
{

    int groundLayer = 1 << 8;//地面图层
    int playerLayer = 1 << 9;
    int monsterLayer = 1 << 10;
    GameObject enemy;//保存怪兽
    public MonsterManager monsterManager;//怪兽状态
    public MonsterAttributes monsterAttributes;//怪兽基础属性
    HpObject hpObject;
    Vector3[] checkVectors;

    NormalAnimation normalAnimation;
    public MonsterMoveInterface monsterMoveInterface;
    public InjuredInterface injuredInterface;
    public Enemy(GameObject enemy,HpObject hpObject)
    {
        this.enemy = enemy;
        monsterAttributes = new MonsterAttributes(5,5,2f,0,1f,1f);
        monsterManager = new MonsterManager(false);
        checkVectors = new Vector3[4];
        checkVectors[0] = new Vector3(1f, 0f, 0f);
        checkVectors[1] = new Vector3(-1f, 0f, 0f);
        checkVectors[2] = new Vector3(0f, -1f, 0f);
        checkVectors[3] = new Vector3(1f, 0f, 0f);

        this.hpObject = hpObject;

        normalAnimation = enemy.GetComponentInChildren<NormalAnimation>();

    }
    public override void SetMoveInterface()//移动接口初始化
    {
        monsterMoveInterface = new MonsterMove();
        monsterMoveInterface.InitObject(enemy, monsterAttributes);

    }
    public override void SetAttackInterface()//攻击接口初始化
    {
        
    }
    public override void SetInjuredInterface()//受伤接口实例化
    {
        injuredInterface = new RoleInjured();
        injuredInterface.SetPlayer(enemy.GetComponent<Rigidbody2D>());
    }
    public void InitInterface()//初始化人物接口
    {
        SetMoveInterface();
        SetAttackInterface();
        SetInjuredInterface();
    }
    public void Updata()
    {
        if (!monsterManager.isInjured && !monsterManager.isDead)
        {
            UpdataMove();
            UpdataAttack();
        }
        else if (monsterManager.isFly && Physics2D.Raycast(enemy.transform.position, checkVectors[2], 0.6f , groundLayer))
        {
            monsterManager.NoInjured();
            monsterManager.isFly = false;
        }
    }
    void UpdataMove()
    {
            monsterMoveInterface.RoleNormalMove(MoveModel.Walk);
            Turnning(); 
    }
    void UpdataAttack()
    {
            Attack();
    }
    public void Turnning()
    {
        if (!monsterManager.isInjured)
        {
            if (monsterMoveInterface.monsterAttributes.toward > 0 && (Physics2D.Raycast(enemy.transform.position, checkVectors[0], 1.5f, groundLayer) ||
                Physics2D.Raycast(enemy.transform.position + checkVectors[3], checkVectors[0], 1.5f, monsterLayer)) ||
               monsterMoveInterface.monsterAttributes.toward < 0 && (Physics2D.Raycast(enemy.transform.position, checkVectors[1], 1.5f, groundLayer) ||
                Physics2D.Raycast(enemy.transform.position - checkVectors[3], checkVectors[1], 1.5f, monsterLayer)) ||
               (monsterMoveInterface.monsterAttributes.toward > 0 && !Physics2D.Raycast(enemy.transform.position + checkVectors[3], checkVectors[2], 0.6f,groundLayer)) ||
               (monsterMoveInterface.monsterAttributes.toward < 0 && !Physics2D.Raycast(enemy.transform.position - checkVectors[3], checkVectors[2], 0.6f,groundLayer)))//四根线判断
            {
                monsterMoveInterface.Turnning();
            }
        }
    }
    public void Attack()
    {
        if (monsterMoveInterface.monsterAttributes.toward > 0 && Physics2D.Raycast(enemy.transform.position, checkVectors[0], 4f, playerLayer) ||
          monsterMoveInterface.monsterAttributes.toward < 0 && Physics2D.Raycast(enemy.transform.position, checkVectors[1], 4f, playerLayer))
        {
            if(monsterMoveInterface.monsterAttributes.toward > 0)
            {
                monsterManager.rightAttack = true;
                monsterManager.leftAttack = false;


            }
            else if(monsterMoveInterface.monsterAttributes.toward < 0)
            {
                monsterManager.rightAttack = false;
                monsterManager.leftAttack = true;

            }
            monsterMoveInterface.RoleNormalMove(MoveModel.Run);
        }
        else
        {
            monsterManager.rightAttack = false ;
            monsterManager.leftAttack = false;
            monsterMoveInterface.RoleNormalMove(MoveModel.Stop);
        }
    }
    public bool Injured(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            monsterManager.isInjured = true;
            
            monsterMoveInterface.RoleNormalMove(MoveModel.Stop);
            if (enemy.transform.localPosition.x > collisionInfo.collider.transform.localPosition.x)
            {
                injuredInterface.NormalInjured(InjuredModel.RightInjured,InjuredModel.Normal);
                monsterManager.RightInjured();
            }
            else
                
            {
                injuredInterface.NormalInjured(InjuredModel.LeftInjured, InjuredModel.Normal);
                monsterManager.LeftInjured();
            }

            return true;
        }
        return false;
    }

    public bool Injured(bool forward, InjuredModel injuredModel)
    {
        monsterManager.isInjured = true;
        hpObject.Injured();
        monsterMoveInterface.RoleNormalMove(MoveModel.Stop);
        if (forward)
        {
            injuredInterface.NormalInjured(InjuredModel.RightInjured,injuredModel);
            monsterManager.RightInjured();
        }
            
        else
        {
            injuredInterface.NormalInjured(InjuredModel.LeftInjured, injuredModel);
            monsterManager.LeftInjured();
        }
            
        
        return true;
    }
    public void SetElement()//设置元素接口
    {
        
        normalAnimation.SetColor(monsterAttributes.color);
    }

    public void SetElement(Color color)//设置元素接口
    {
        monsterAttributes.color = color;
        normalAnimation.SetColor(color);
    }

    public IEnumerator StopMove(float stopTime)
    {
        
        yield return new WaitForSeconds(stopTime);
        monsterManager.isFly = true;
    }
}
