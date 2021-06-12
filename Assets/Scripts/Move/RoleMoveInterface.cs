using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoleMoveInterface : MoveInterface
{
    public abstract void InitObject(GameObject gameObject, RoleAttribute roleAttributes);
    public abstract void RoleFastMove(MoveModel model);//快速移动，还没加上限制条件

    public abstract void RoleClimeMove(MoveModel model);//攀爬移动
    public abstract void RoleJump();//反抗重力跳跃
    public abstract void RoleStopJump();//停止跳跃
}
