using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoleMoveManager
{
    public bool leftMove;//左快速移动
    public bool rightMove;//右快速移动
    public bool leftFastMove;//左快速移动
    public bool rightFastMove;//右快速移动
    public bool jump;//跳跃
    public bool stop;//停止
    public bool leftInjured;//被左方攻击
    public bool rightInjured;//被右方攻击
    public bool isClimb;//是否爬行
    public bool ClimbAnimation;//爬行动画是否开始
    public bool isPickOut;//是否进行了挑击
    public bool isSprintAttack;//是否进行了冲刺攻击
    public bool isPickOutAnimation;//是否进行了挑击动画
    public bool isSprintAttackAnimation;//是否进行了冲刺攻击动画
    public bool isDead;//死亡
    public bool isSplit;//分裂

    public int jumpTimes;//跳跃次数
    public float moveRate;
    public int attacktDireaction;//攻击方向
    public void AllInit(int roleJumpTimes)//初始化
    {
        MoveInit();
        jump = false;//跳跃
        leftFastMove = false;
        rightFastMove = false;
        isClimb = false;
        ClimbAnimation = false;
        isPickOut = false;
        isSprintAttack = false;
        isPickOutAnimation = false;
        isSprintAttackAnimation = false;
        isDead = false;
        isSplit = false;
        jumpTimes = roleJumpTimes;//最大跳跃次数
        moveRate = 1f;//移动倍率
        attacktDireaction = 0;
    }

    public int istest()
    {
        if(leftMove)
        {
            return 0;
        }
        else if(rightMove)
        {
            return 1;
        }
        else if (leftFastMove)
        {
            return 2;
        }
        else if (rightFastMove)
        {
            return 3;
        }
        else if (jump)
        {
            return 4;
        }
        else if(leftInjured)
        {
            return 5;
        }
        else if(rightInjured)
        {
            return 6;
        }
        else if(stop)
        {
            return 7;
        }
        return 8;
    }

    public bool isStop()
    {
        if (leftMove||rightMove||leftFastMove||rightFastMove||jump)
        {
            return false;
        }
        return true;
    }
    public void MoveInit()
    {
        leftMove = false;//左快速移动
        rightMove = false;//右快速移动
        leftFastMove = false;//左快速移动
        rightFastMove = false;//右快速移动
        stop = false;//停止
        attacktDireaction = 0;
    }
}
