using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager
{
    public bool isInjured = false;
    public bool isFly = false;
    public bool leftAttack = false;
    public bool rightAttack = false;
    public bool leftInjured = false;
    public bool rightInjured = false;
    public bool isDead = false;
    public MonsterManager(bool flag)
    {
        isInjured = false;
        isFly = false;
        leftAttack = false;
        rightAttack = false;
        leftInjured = false;
        rightInjured = false;
    }
    public void RightInjured()
    {
        rightInjured = true;
        leftInjured = false ;
    }

    public void LeftInjured()
    {
        rightInjured = false;
        leftInjured = true;
    }

    public void NoInjured()
    {
        rightInjured = false;
        leftInjured = false;
        isInjured = false;
    }
}
