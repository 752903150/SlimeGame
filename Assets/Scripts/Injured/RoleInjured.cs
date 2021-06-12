using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleInjured : InjuredInterface
{
    Rigidbody2D rigidbody2D;
    public void SetPlayer(Rigidbody2D rigidbody2D)
    {
        this.rigidbody2D = rigidbody2D;
    }
    public void NormalInjured(InjuredModel injured,InjuredModel attackModel)
    {
        if(injured == InjuredModel.LeftInjured)
        {
            NormalLeftInjured(attackModel);
        }
        else if(injured == InjuredModel.RightInjured)
        {
            NormalRightInjured(attackModel);
        }
    }
    void NormalLeftInjured(InjuredModel attackModel)
    {
        if(attackModel == InjuredModel.Normal)
        {
            rigidbody2D.velocity = new Vector3(-4f, 4f, 0f);
        }
        else if(attackModel == InjuredModel.SwordPickOut)
        {
            rigidbody2D.velocity = new Vector3(-2f, 6f, 0f);
        }
    }
    void NormalRightInjured(InjuredModel attackModel)
    {
        if (attackModel == InjuredModel.Normal)
        {
            rigidbody2D.velocity = new Vector3(4f, 4f, 0f);
        }
        else if (attackModel == InjuredModel.SwordPickOut)
        {
            rigidbody2D.velocity = new Vector3(2f, 6f, 0f);
        }
    }
}
